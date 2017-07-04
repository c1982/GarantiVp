namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;

    public class GVPSClient : IGVPSBuilder
    {
        private GVPSRequest request;

        private const GVPSRequestModeEnum REQUEST_TEST_MODE = GVPSRequestModeEnum.Test;
        private const GVPSRequestModeEnum REQUEST_PROD_MODE =  GVPSRequestModeEnum.Production;
        private const string REQUEST_USER_PROVAUT = "PROVAUT"; //Provizyon kullanıcısı
        private const string REQUEST_USER_PROVRFN = "PROVRFN"; //İptal ve İade işlemlerinde kullanılır

        private string REQUEST_URL;
        private string REQUEST_URL_FOR_3D;
        private string hashedPassword;
        private Encoding usingEncoding = Encoding.GetEncoding("iso-8859-9");

        public Encoding UsingEncoding
        {
            get
            {
                return usingEncoding;
            }

            private set
            {
                usingEncoding = value;
            }
        }

        public GVPSClient(bool test = false)
        {
            request = new GVPSRequest();
            var AsmName = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName();
            request.Version = AsmName.Name + " v" + AsmName.Version.Major.ToString() + "." + AsmName.Version.Minor.ToString();
            if (request.Version.Length > 16)
                request.Version = "v" + AsmName.Version.Major.ToString() + "." + AsmName.Version.Minor.ToString();
            //request.Version = "v0.01";

            request.Terminal = new GVPSTerminal();
            request.Terminal.ProvUserID = REQUEST_USER_PROVAUT;

            request.Transaction = new GVPSTransaction();
            request.Transaction.Type = GVPSTransactionTypeEnum.sales;
            request.Transaction.MotoInd = GVPSMotoIndEnum.ECommerce;
            request.Transaction.CurrencyCode = GVPSCurrencyCodeEnum.TRL;

            this.Test(false);
        }

        public IGVPSBuilder Server(string posUrl)
        {
            if (!String.IsNullOrEmpty(posUrl))
                REQUEST_URL = posUrl;

            return this;
        }

        public IGVPSBuilder Test(bool isTest)
        {
            if (isTest)
            {
                this.REQUEST_URL = "https://sanalposprovtest.garanti.com.tr/VPServlet";
                this.REQUEST_URL_FOR_3D = "https://sanalposprovtest.garanti.com.tr/servlet/gt3dengine";
                request.Mode = REQUEST_TEST_MODE;
            }
            else
            {
                this.REQUEST_URL = "https://sanalposprov.garanti.com.tr/VPServlet";
                this.REQUEST_URL_FOR_3D = "https://sanalposprov.garanti.com.tr/servlet/gt3dengine";
                request.Mode = REQUEST_PROD_MODE;
            }
            return this;
        }

        public IGVPSBuilder Company(string terminalId, string MerchantID, string userID, string userPassword, string SubMerchantID = null)
        {
            request.Terminal.ID = terminalId; //isRequireZero(terminalId, 9);
            request.Terminal.MerchantID = MerchantID;
            request.Terminal.SubMerchantID = SubMerchantID;
            request.Terminal.UserID = userID;
            this.hashedPassword = GetSHA1(userPassword + isRequireZero(terminalId, 9)).ToUpper();
            return this;
        }

        public IGVPSBuilder Customer(string customerMail, string customerIP)
        {
            request.Customer = request.Customer ?? new GVPSCustomer();

            request.Customer.EmailAddress = customerMail;
            request.Customer.IPAddress = customerIP;

            return this;
        }

        public IGVPSBuilder CreditCard(string number, string cvv2, int month, int year)
        {
            request.Card = request.Card ?? new GVPSCard();

            request.Card.Number = number;
            request.Card.CVV2 = cvv2.ToString();
            request.Card.ExpireDate = String.Format("{0}{1}", isRequireZero(month), isRequireZero(year));

            return this;
        }

        public IGVPSBuilder Order(string orderID, string groupID = "")
        {
            request.Order = request.Order ?? new GVPSOrder();
            request.Order.OrderID = orderID;
            request.Order.GroupID = groupID;
            return this;
        }

        public IGVPSBuilder AddOrderAddress(GVPSAddressTypeEnum type, string city, string district, string addressText, string phone, string name, string lastName, string Company = null, string postalCode = null)
        {
            var address = new GVPSAddress();
            address.Type = type;
            address.City = city;
            address.District = district;
            address.Text = addressText;
            address.PhoneNumber = phone;
            address.Name = name;
            address.LastName = lastName;
            address.Company = Company;
            address.PostalCode = postalCode;
            return AddOrderAddress(address);
        }

        public IGVPSBuilder AddOrderAddress(GVPSAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            if (request.Order == null)
            {
                throw new ArgumentException("Order must first be defined.", "Order");
            }
            if (address.Type == GVPSAddressTypeEnum.Unspecified)
            {
                throw new ArgumentOutOfRangeException("Type", "The address type must be defined.");
            }
            request.Order.AddressList = request.Order.AddressList ?? new GVPSAddressList();
            request.Order.AddressList.Address = request.Order.AddressList.Address ?? new GVPSAddress[] { };
            var Addresses = new List<GVPSAddress>();
            Addresses.AddRange(request.Order.AddressList.Address);
            if (Addresses.Where(e => e.Type.Equals(address.Type)).Count() > 0)
            {
                throw new ArgumentException("Address type already defined.");
            }

            Addresses.Add(address);
            request.Order.AddressList.Address = Addresses.ToArray();
            return this;
        }

        public IGVPSBuilder AddOrderItem(uint number, string productCode, string productId, double price, double quantity, string description = null, double totalAmount = 0.0)
        {
            if ((number > 99) || (number < 1))
            {
                throw new ArgumentOutOfRangeException("number", "Must be between 1 and 99");
            }
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Must be greater than 0");
            }
            if (totalAmount < 0)
            {
                throw new ArgumentOutOfRangeException("totalAmount", "Must be greater than 0");
            }
            totalAmount = (totalAmount == 0.0) ? (price * quantity) : totalAmount;
            var item = new GVPSItem();
            item.Description = description;
            item.Number = number;
            item.Price = (ulong)(Math.Round(price, 2) * 100);
            item.ProductCode = productCode ;
            item.ProductID = productId;
            item.Quantity = (ulong)(Math.Round(quantity, 2) * 100);
            item.TotalAmount = (ulong)(Math.Round(totalAmount, 2) * 100);
            return AddOrderItem(item);
        }

        public IGVPSBuilder AddOrderItem(GVPSItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (request.Order == null)
            {
                throw new ArgumentException("Order must first be defined.", "Order");
            }
            if ((item.Number > 99) || (item.Number < 1))
            {
                throw new ArgumentOutOfRangeException("Number", "Must be between 1 and 99");
            }
            if (item.Quantity <= 0)
            {
                throw new ArgumentOutOfRangeException("Quantity", "Must be greater than 0");
            }
            if (item.TotalAmount < 0)
            {
                throw new ArgumentOutOfRangeException("TotalAmount", "Must be greater than 0");
            }
            request.Order.ItemList = request.Order.ItemList ?? new GVPSItemList();
            request.Order.ItemList.Item = request.Order.ItemList.Item ?? new GVPSItem[] { };
            var Items = new List<GVPSItem>();
            Items.AddRange(request.Order.ItemList.Item);
            Items.Add(item);
            request.Order.ItemList.Item = Items.ToArray();
            return this;
        }

        public IGVPSBuilder AddOrderComment(uint number, string text)
        {
            var comment = new GVPSComment();
            comment.Number = number;
            comment.Text = text;
            return AddOrderComment(comment);
        }

        public IGVPSBuilder AddOrderComment(GVPSComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            if (request.Order == null)
            {
                throw new ArgumentException("Order must first be defined.", "Order");
            }
            if (comment.Number <= 0)
            {
                throw new ArgumentOutOfRangeException("Number");
            }
            if (!string.IsNullOrEmpty(comment.Text) && (comment.Text.Length > 20))
            {
                throw new ArgumentException("Comment text field must be max 20 char.");
            }
            request.Order.CommentList = request.Order.CommentList ?? new GVPSCommentList();
            request.Order.CommentList.Comment = request.Order.CommentList.Comment ?? new GVPSComment[] { };
            var Comments = new List<GVPSComment>();
            Comments.AddRange(request.Order.CommentList.Comment);
            if(Comments.Where(e => e.Number.Equals(comment.Number)).Count() > 0)
            {
                throw new ArgumentException("Comment number already defined.");
            }
            Comments.Add(comment);
            request.Order.CommentList.Comment = Comments.ToArray();
            return this;
        }

        public IGVPSBuilder Amount(double totalAmount, GVPSCurrencyCodeEnum currencyCode = GVPSCurrencyCodeEnum.TRL)
        {
            request.Transaction.Amount = (ulong)(Math.Round(totalAmount, 2) * 100);
            request.Transaction.CurrencyCode = currencyCode;

            return this;
        }

        public IGVPSBuilder Installment(int installment)
        {
            request.Transaction.InstallmentCnt = installment <= 0 ? string.Empty : installment.ToString();

            return this;
        }

        public IGVPSBuilder Delay(int day)
        {
            request.Transaction.DelayDayCount = day.ToString();

            return this;
        }

        public IGVPSBuilder DownPaymentRate(int rate)
        {
            request.Transaction.DownPaymentRate = rate.ToString();
            return this;
        }

        public GVPSResponse Sales()
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if ((request.Order == null) || string.IsNullOrWhiteSpace(request.Order.OrderID))
                throw new ArgumentNullException("Order ID");
            if ((request.Card == null) || string.IsNullOrWhiteSpace(request.Card.Number))
                throw new ArgumentNullException("Card Number");
            if ((request.Terminal == null) || string.IsNullOrWhiteSpace(request.Terminal.ID))
                throw new ArgumentNullException("Terminal ID");
            if (request.Transaction == null)
                throw new ArgumentNullException("Transaction");


            request.Transaction.Type = GVPSTransactionTypeEnum.sales;
            request.Transaction.CardholderPresentCode =  GVPSCardholderPresentCodeEnum.Normal;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this.hashedPassword
                                                    ).ToUpper();

            return Send();
        }

        public GVPSResponse Refund()
        {
            request.Transaction.Type = GVPSTransactionTypeEnum.refund;
            request.Terminal.ProvUserID = REQUEST_USER_PROVRFN;
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal ;
            request.Transaction.MotoInd = GVPSMotoIndEnum.H;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                request.Terminal.ID +
                                                request.Transaction.Amount +
                                                this.hashedPassword
                                                ).ToUpper();

            return Send();
        }

        public GVPSResponse RefundCancel(string refundRetrefNum)
        {
            return Cancel(refundRetrefNum);
        }

        public GVPSResponse Cancel(string retrefNum)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (request.Order == null)
                throw new ArgumentNullException("Order");
            if (string.IsNullOrWhiteSpace(request.Order.OrderID))
                throw new ArgumentNullException("OrderID");
            if (request.Terminal == null)
                throw new ArgumentNullException("Terminal");
            if (string.IsNullOrWhiteSpace(request.Terminal.ID))
                throw new ArgumentNullException("Terminal ID");
            if (request.Transaction == null)
                throw new ArgumentNullException("Transaction");
            request.Transaction.Type = GVPSTransactionTypeEnum.@void;
            request.Terminal.ProvUserID = REQUEST_USER_PROVRFN;
            request.Transaction.OriginalRetrefNum = retrefNum;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                request.Terminal.ID +
                                                request.Transaction.Amount +
                                                this.hashedPassword
                                                ).ToUpper();

            return Send();
        }

        public GVPSResponse Preauth()
        {
            request.Transaction.Type = GVPSTransactionTypeEnum.preauth;
            request.Transaction.MotoInd = GVPSMotoIndEnum.ECommerce;
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal;
            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this.hashedPassword
                                                    ).ToUpper();
            return Send();
        }

        public GVPSResponse PostauthCancel(string retrefNum)
        {
            return Cancel(retrefNum);
        }

        public GVPSResponse Postauth(string retrefNum)
        {
            request.Transaction.Type = GVPSTransactionTypeEnum.postauth;
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal;
            request.Transaction.OriginalRetrefNum = retrefNum;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                        request.Terminal.ID +
                                        ((request.Card == null) ? "" : request.Card.Number) +
                                        request.Transaction.Amount +
                                        this.hashedPassword
                                        ).ToUpper();

            return Send();
        }

        public GVPSResponse Verification(string TCKN)
        {
            request.Transaction.Verification = request.Transaction.Verification ?? new GVPSVerification();

            request.Transaction.Type = GVPSTransactionTypeEnum.identifyinq;
            request.Transaction.Verification.Identity = TCKN;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this.hashedPassword
                                                    ).ToUpper();

            return Send();
        }

        public XmlElement Sale3DRequest(string storeKeyFor3D, Uri successUri, Uri failUri, ushort RefreshTime = 0, string Lang = "tr")
        {
            XmlElement ret = null;
            try
            {
                if (request == null)
                    throw new ArgumentNullException("request");
                if ((request.Order == null) || string.IsNullOrWhiteSpace(request.Order.OrderID))
                    throw new ArgumentNullException("Order ID");
                if ((request.Card == null) || string.IsNullOrWhiteSpace(request.Card.Number))
                    throw new ArgumentNullException("Card Number");
                if ((request.Terminal == null) || string.IsNullOrWhiteSpace(request.Terminal.ID))
                    throw new ArgumentNullException("Terminal ID");
                if (request.Transaction == null)
                    throw new ArgumentNullException("Transaction");


                request.Transaction.Type = GVPSTransactionTypeEnum.sales;
                request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Secure3D;
                var OperationType = request.Transaction.Type.GetXmlEnumName();
                if (string.IsNullOrWhiteSpace(OperationType))
                    throw new ArgumentException("Transaction type not know; " + request.Transaction.Type.ToString());
                //terminalId + orderid + amount + okurl + failurl + islemtipi + taksit + storekey + provUser.getPasswordText()
                //TerminalID + OrderID + Amount + SuccessURL + ErrorURL + Type + InstallmentCount + StoreKey + SecurityData
                var Secure3DHash = GetSHA1(request.Terminal.ID
                                                    + request.Order.OrderID
                                                    + request.Transaction.Amount.ToString()
                                                    + successUri.ToString()
                                                    + failUri.ToString()
                                                    + OperationType
                                                    + (request.Transaction.InstallmentCnt ?? "")
                                                    + storeKeyFor3D
                                                    + this.hashedPassword
                                                    ).ToUpper();

                var xDoc = new XmlDocument();
                var xF = xDoc.CreateElement("form");
                xF.SetAttribute("method", "POST");
                xF.SetAttribute("action", REQUEST_URL_FOR_3D);

                //Root
                xF.AddInput("mode", request.Mode.GetXmlEnumName()); //Required
                xF.AddInput("apiversion", request.Version); //Required
                xF.AddInput("secure3dhash", Secure3DHash); //Required
                xF.AddInput("refreshtime", RefreshTime.ToString()); //Required
                xF.AddInput("lang", Lang); //Required
                xF.AddInput("secure3dsecuritylevel", "3D"); //Required

                //Terminal information
                xF.AddInput("terminalprovuserid", request.Terminal.ProvUserID);
                xF.AddInput("terminaluserid", request.Terminal.UserID); //Required
                xF.AddInput("terminalmerchantid", request.Terminal.MerchantID); //Required
                xF.AddInput("terminalid", request.Terminal.ID); //Required

                //Transaction information
                xF.AddInput("txntimestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
                xF.AddInput("txntype", request.Transaction.Type.GetXmlEnumName()); //Required
                xF.AddInput("txnamount", request.Transaction.Amount.ToString()); //Required
                xF.AddInput("txncurrencycode", request.Transaction.CurrencyCode.GetXmlEnumName()); //Required
                xF.AddInput("txninstallmentcount", request.Transaction.InstallmentCnt??"0");
                //xF.AddInput("txndownpayrate", request.Transaction.DownPaymentRate);
                //xF.AddInput("txndelaydaycnt", request.Transaction.DelayDayCount);
                //xF.AddInput("txncardholderpresentcode", request.Transaction.CardholderPresentCode.GetXmlEnumName());
                //request.Transaction.MotoInd = GVPSMotoIndEnum.H;
                //xF.AddInput("txnmotoind", request.Transaction.MotoInd.GetXmlEnumName()); //Required

                //TODO Transaction rewards
                //if ((request.Transaction != null) && (request.Transaction.RewardList != null) && (request.Transaction.RewardList.Reward != null))
                //{
                //    var itemCount = request.Transaction.RewardList.Reward.Count();
                //    xF.AddInput("txnrewardcount", itemCount.ToString());
                //    foreach (var item in request.Transaction.RewardList.Reward)
                //    {
                //        xF.AddInput("txnrewardtype" + itemCount.ToString(), item.Type);
                //        xF.AddInput("txnrewardgainedamount" + itemCount.ToString(), item.GainedAmount);
                //        xF.AddInput("txnrewardusedamount" + itemCount.ToString(), item.UsedAmount);
                //    }
                //}

                //TODO Transaction cheques
                //if ((request.Transaction != null) && (request.Transaction.ChequeList != null) && (request.Transaction.ChequeList.Cheque != null))
                //{
                //    var itemCount = request.Transaction.ChequeList.Cheque.Count();
                //    xF.AddInput("txnchequecount", itemCount.ToString());
                //    foreach (var item in request.Transaction.ChequeList.Cheque)
                //    {
                //        xF.AddInput("txnchequetype" + itemCount.ToString(), item.Type);
                //        xF.AddInput("txnchequeamount" + itemCount.ToString(), item.Amount);
                //        xF.AddInput("txnchequebitmap" + itemCount.ToString(), item.Bitmap);
                //        xF.AddInput("txnchequeid" + itemCount.ToString(), item.ID);
                //        xF.AddInput("txnchequecount" + itemCount.ToString(), item.Count);
                //    }
                //}

                //Uri
                xF.AddInput("successurl", successUri.ToString());
                xF.AddInput("errorurl", failUri.ToString());

                //Customer
                xF.AddInput("customeremailaddress", request.Customer.EmailAddress);
                xF.AddInput("customeripaddress", request.Customer.IPAddress.ToString());

                //Card
                xF.AddInput("cardnumber", request.Card.Number); //Required
                xF.AddInput("cardcvv2", request.Card.CVV2); //Required
                xF.AddInput("cardexpiredatemonth", ushort.Parse(request.Card.ExpireDate.Substring(0, 2)).ToString()); //Required
                xF.AddInput("cardexpiredateyear", request.Card.ExpireDate.Substring(2, 2)); //Required
                //xF.AddInput("cardholder", request.Card.CardHolder);


                //Order information
                xF.AddInput("orderid", request.Order.OrderID); //Required
                xF.AddInput("ordergroupid", request.Order.GroupID);
                xF.AddInput("orderdescription", request.Order.Description);

                //Order items
                if ((request.Order != null) && (request.Order.ItemList != null) && (request.Order.ItemList.Item != null))
                {
                    var itemCount = request.Order.ItemList.Item.Count();
                    xF.AddInput("orderitemcount", itemCount.ToString());
                    foreach (var item in request.Order.ItemList.Item)
                    {
                        xF.AddInput("orderitemnumber" + itemCount.ToString(), item.Number.ToString());
                        xF.AddInput("orderitemproductid" + itemCount.ToString(), item.ProductID);
                        xF.AddInput("orderitemproductcode" + itemCount.ToString(), item.ProductCode);
                        xF.AddInput("orderitemquantity" + itemCount.ToString(), item.Quantity.ToString());
                        xF.AddInput("orderitemprice" + itemCount.ToString(), item.Price.ToString());
                        xF.AddInput("orderitemtotalamount" + itemCount.ToString(), item.TotalAmount.ToString());
                        xF.AddInput("orderitemdescription" + itemCount.ToString(), item.Description);
                    }
                }

                //Order comments
                if ((request.Order != null) && (request.Order.CommentList != null) && (request.Order.CommentList.Comment!= null))
                {
                    var itemCount = request.Order.CommentList.Comment.Count();
                    xF.AddInput("orderaddresscount", itemCount.ToString());
                    foreach (var item in request.Order.CommentList.Comment)
                    {
                        xF.AddInput("ordercommentnumber" + itemCount.ToString(), item.Number.ToString());
                        xF.AddInput("ordercommenttext" + itemCount.ToString(), item.Text);
                    }
                }

                //Order addresses
                if ((request.Order != null) && (request.Order.AddressList != null) && (request.Order.AddressList.Address != null))
                {
                    var itemCount = request.Order.AddressList.Address.Count();
                    xF.AddInput("orderaddresscount", itemCount.ToString());
                    foreach (var item in request.Order.AddressList.Address)
                    {
                        xF.AddInput("orderaddresscity" + itemCount.ToString(), item.City);
                        xF.AddInput("orderaddresscompany" + itemCount.ToString(), item.Company);
                        xF.AddInput("orderaddresscountry" + itemCount.ToString(), item.Country);
                        xF.AddInput("orderaddressdistrict" + itemCount.ToString(), item.District);
                        xF.AddInput("orderaddressfaxnumber" + itemCount.ToString(), item.FaxNumber);
                        xF.AddInput("orderaddressgsmnumber" + itemCount.ToString(), item.GSMNumber);
                        xF.AddInput("orderaddresslastname" + itemCount.ToString(), item.LastName);
                        xF.AddInput("orderaddressphonenumber" + itemCount.ToString(), item.PhoneNumber);
                        xF.AddInput("orderaddresspostalcode" + itemCount.ToString(), item.PostalCode);
                        xF.AddInput("orderaddresstext" + itemCount.ToString(), item.Text);
                        xF.AddInput("orderaddresstype" + itemCount.ToString(), item.Type.GetXmlEnumName());
                    }
                }

                //TODO money card information
                //xF.AddInput("moneyccdisc", request.Transaction.MoneyCard.);
                //xF.AddInput("moneyextradisc", request.Transaction.MoneyCard.);
                //xF.AddInput("moneyinvoice", request.Transaction.MoneyCard.);
                //xF.AddInput("moneypayment", request.Transaction.MoneyCard.);
                //xF.AddInput("moneyproductbaseddisc", request.Transaction.MoneyCard.);

                //TODO utility information
                if(request.Transaction.UtilityPayment!= null)
                {
                    xF.AddInput("utilitypayinvoiceid", request.Transaction.UtilityPayment.InvoiceID);
                    xF.AddInput("utilitypaysubscode", request.Transaction.UtilityPayment.SubscriberCode);
                    //xF.AddInput("utilitypaytype", request.Transaction.UtilityPayment);
                }

                //GSM information
                if (request.Transaction.GSMUnitSales != null)
                {
                    xF.AddInput("gsmquantity", request.Transaction.GSMUnitSales.Quantity);
                    xF.AddInput("gsmsalesamnt", request.Transaction.GSMUnitSales.Amount.ToString());
                    xF.AddInput("gsmsalesunitid", request.Transaction.GSMUnitSales.UnitID);
                }
                ret = xF;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public GVPSResponse Sales3DEvaluatesResponseAndGetProvision(IDictionary<string, string[]> formDataDic, string storeKeyFor3D, Uri successUri, Uri failUri)
        {
            if (formDataDic == null)
                throw new ArgumentNullException("postData");
            /*
                +mdstatus
                mderrormessage
                errmsg
                clientid
                oid
                response
                procreturncode
                version
             */

            /* GARANTI 3D ALANLAR
                +cardnumber
                +cardcvv2
                +cardexpiredatemonth
                +cardexpiredateyear
                +cardholder

                +secure3dauthenticationcode
                +secure3dsecuritylevel
                +secure3dtxnid
                +secure3drnd
                +secure3dhash
                +secure3DRecordKey

                +mode
                +apiversion
                +customeremailaddress
                +customeripaddress

                +terminalid
                +terminalmerchantid

                -?firmacardno
                +txncurrencycode
                -?companyname
                +errorurl
                +successurl
                +terminalprovuserid
                +terminaluserid
                +orderid
                -?refreshtime
                -?lang
                +ordergroupid
                +orderdescription

                +txnamount
                +txntype
                +txninstallmentcount
                txninstallmentperiod
                txntimestamp
                +txndownpayrate
                +txndelaydaycnt
                txncardholderpresentcode
                txndescription
                +txnmotoind
                
                utilitypayinvoiceid
                utilitypaysubscode
                utilitypaytype
                
                gsmquantity
                gsmsalesamnt
                gsmsalesunitid
                
                moneyccdisc
                moneyextradisc
                moneyinvoice
                moneypayment
                moneyproductbaseddisc

                +orderitemcount
                +orderitemnumber+ Count
                +orderitemproductid+ Count
                +orderitemproductcode+ Count
                +orderitemquantity+ Count
                +orderitemprice+ Count
                +orderitemtotalamount+ Count
                +orderitemdescription+ Count

                +orderaddresscount
                +orderaddresscity+ Count
                +orderaddresscompany+ Count
                +orderaddresscountry+ Count
                +orderaddressdistrict+ Count
                +orderaddressfaxnumber+ Count
                +orderaddressgsmnumber+ Count
                +orderaddresslastname+ Count
                +orderaddressname+ Count
                +orderaddressphonenumber+ Count
                +orderaddresspostalcode+ Count
                +orderaddresstext+ Count
                +orderaddresstype+ Count

                +ordercommentcount
                +ordercommenttext+ Count
                +ordercommentnumber+ Count

                txnrewardcount
                txnrewardtype+ Count
                txnrewardgainedamount+ Count
                txnrewardusedamount+ Count

                txnchequecount
                txnchequetype+ Count
                txnchequeamount+ Count
                txnchequebitmap+ Count
                txnchequeid+ Count
                txnchequecount+ Count
             */

            //TODO Fill GVPSResponse from posted form data...
            var ret = new GVPSResponse();

            //internal
            ret.RawRequest = null;
            ret.RawResponse = string.Join("&", formDataDic.Select(e => string.Join("&", e.Value.Select(v => WebUtility.UrlEncode(e.Key) + "=" + WebUtility.UrlEncode(v)).ToArray())));

            //Root
            //TODO ret.ChannelCode = formDataDic.Get("");
            ret.Mode = formDataDic.Get("mode").GetValueFromXmlEnumName<GVPSRequestModeEnum>();
            ret.Version = formDataDic.Get("apiversion");
            var secure3DHash = formDataDic.Get("secure3dhash");
            var refreshTime = formDataDic.Get("refreshtime");
            var lang = formDataDic.Get("lang");
            GVPSMdStatusEnum mdStatus = formDataDic.Get("mdstatus").GetValueFromXmlEnumName<GVPSMdStatusEnum>();
            var mdErrorMessage = formDataDic.Get("mderrormessage");
            var errMsg = formDataDic.Get("errmsg");
            var response = formDataDic.Get("response");
            var procreturncode = formDataDic.Get("procreturncode");

            var ExMessage = "";
            switch(mdStatus)
            {
                case GVPSMdStatusEnum.Full:
                    break;
                case GVPSMdStatusEnum.HalfBankUnknow:
                    break;
                case GVPSMdStatusEnum.HalfCardHolderOrBankUnknow:
                    break;
                case GVPSMdStatusEnum.HalfVerificationTest:
                    break;
                case GVPSMdStatusEnum.Fail3DSecureVerificationFailed:
                    ExMessage = "3D secure verification failed.";
                    break;
                case GVPSMdStatusEnum.FailSecureError:
                    ExMessage = "Secure error.";
                    break;
                case GVPSMdStatusEnum.FailSystemError:
                    ExMessage = "System error.";
                    break;
                case GVPSMdStatusEnum.FailUnknowCardNo:
                    ExMessage = "Unknow card no.";
                    break;
                case GVPSMdStatusEnum.FailVerification:
                    ExMessage = "Verification failed.";
                    break;
                case GVPSMdStatusEnum.Undefined:
                    ExMessage = "Undefined md status.";
                    break;
                default:
                    throw new GVPSExcetion("Unknow md status.");
            }
            if (!string.IsNullOrWhiteSpace(ExMessage))
            {
                ExMessage += " (" + response + " " + procreturncode + " " + errMsg + ")";
                throw new GVPSExcetion(ExMessage);
            }

            //Card
            ret.Card = new GVPSCard();
            ret.Card.CardHolder = formDataDic.Get("cardholder");
            ret.Card.CVV2 = formDataDic.Get("cardcvv2");
            ret.Card.ExpireDate = (formDataDic.Get("cardexpiredatemonth") ?? "")
                                + (formDataDic.Get("cardexpiredateyear") ?? "");
            ret.Card.Number = formDataDic.Get("cardnumber");
            if (string.IsNullOrWhiteSpace(ret.Card.CardHolder)
                && string.IsNullOrWhiteSpace(ret.Card.CVV2)
                && string.IsNullOrWhiteSpace(ret.Card.ExpireDate)
                && string.IsNullOrWhiteSpace(ret.Card.Number)
                )
                ret.Card = null;

            //Customer
            ret.Customer = new GVPSCustomer();
            ret.Customer.EmailAddress = formDataDic.Get("customeremailaddress");
            ret.Customer.IPAddress = formDataDic.Get("customeripaddress");

            //Order
            ret.Order = new GVPSOrder();
            ret.Order.GroupID = formDataDic.Get("ordergroupid");
            //ret.Order.OrderID = formDataDic.Get("orderid");
            ret.Order.OrderID = formDataDic.Get("oid");
            ret.Order.Description = formDataDic.Get("orderdescription");

            //Order addresses
            var addressCount = 0;
            if (int.TryParse(formDataDic.Get("orderaddresscount"), out addressCount))
            {
                ret.Order.AddressList = new GVPSAddressList();
                var AddressList = new List<GVPSAddress>();
                for (int i = 0; i < addressCount; i++)
                {
                    AddressList.Add(new GVPSAddress());
                    AddressList.Last().City = formDataDic.Get("orderaddresscity" + i.ToString());
                    AddressList.Last().Company = formDataDic.Get("orderaddresscompany" + i.ToString());
                    AddressList.Last().Country = formDataDic.Get("orderaddresscountry" + i.ToString());
                    AddressList.Last().District = formDataDic.Get("orderaddressdistrict" + i.ToString());
                    AddressList.Last().FaxNumber = formDataDic.Get("orderaddressfaxnumber" + i.ToString());
                    AddressList.Last().GSMNumber = formDataDic.Get("orderaddressgsmnumber" + i.ToString());
                    AddressList.Last().LastName = formDataDic.Get("orderaddresslastname" + i.ToString());
                    AddressList.Last().Name = formDataDic.Get("orderaddressname" + i.ToString());
                    AddressList.Last().PhoneNumber = formDataDic.Get("orderaddressphonenumber" + i.ToString());
                    AddressList.Last().PostalCode = formDataDic.Get("orderaddresspostalcode" + i.ToString());
                    AddressList.Last().Text = formDataDic.Get("orderaddresstext" + i.ToString());
                    AddressList.Last().Type = formDataDic.Get("orderaddresstype" + i.ToString()).GetValueFromXmlEnumName<GVPSAddressTypeEnum>();
                }
                ret.Order.AddressList.Address = AddressList.ToArray();
            }

            //Order Comment
            var commentCount = 0;
            if (int.TryParse(formDataDic.Get("orderaddresstype"), out commentCount))
            {
                ret.Order.CommentList = new GVPSCommentList();
                var CommentList = new List<GVPSComment>();
                for (int i = 0; i < commentCount; i++)
                {
                    CommentList.Add(new GVPSComment());
                    CommentList.Last().Number = uint.Parse(formDataDic.Get("ordercommentnumber" + i.ToString()));
                    CommentList.Last().Text = formDataDic.Get("ordercommenttext" + i.ToString());
                }
                ret.Order.CommentList.Comment = CommentList.ToArray();
            }

            //Order Items
            var orderItemCount = 0;
            if (int.TryParse(formDataDic.Get("orderitemcount"), out orderItemCount))
            {
                ret.Order.ItemList = new GVPSItemList();
                var ItemList = new List<GVPSItem>();
                for (int i = 0; i < orderItemCount; i++)
                {
                    ItemList.Add(new GVPSItem());
                    ItemList.Last().Description = formDataDic.Get("orderitemdescription" + i.ToString());
                    ItemList.Last().Number = uint.Parse(formDataDic.Get("orderitemnumber" + i.ToString()));
                    ItemList.Last().Price = ulong.Parse(formDataDic.Get("orderitemprice" + i.ToString()));
                    ItemList.Last().ProductCode = formDataDic.Get("orderitemproductcode" + i.ToString());
                    ItemList.Last().ProductID = formDataDic.Get("orderitemproductid" + i.ToString());
                    ItemList.Last().Quantity = ulong.Parse(formDataDic.Get("orderitemquantity" + i.ToString()));
                    ItemList.Last().TotalAmount = ulong.Parse(formDataDic.Get("orderitemtotalamount" + i.ToString()));
                }
                ret.Order.ItemList.Item = ItemList.ToArray();
            }

            //TODO Order Recurring
            //ret.Order.Recurring = new GVPSRecurring();
            //ret.Order.Recurring.FrequencyInterval = ushort.Parse(formDataDic.Get(""));
            //ret.Order.Recurring.FrequencyType = GVPSFrequencyTypeEnum.Unspecified;
            //ret.Order.Recurring.PaymentList = new GVPSReccuringPaymentList();
            //var ReccurringPaymentList = new List<GVPSReccurringPayment>();
            //ReccurringPaymentList.Add(new GVPSReccurringPayment());
            //ReccurringPaymentList.Last().Amount = formDataDic.Get("");
            //ReccurringPaymentList.Last().PaymentNum = ushort.Parse(formDataDic.Get(""));
            //ret.Order.Recurring.PaymentList.Payment = ReccurringPaymentList.ToArray();

            //TODO SettlementInq
            //ret.SettlementInq = new GVPSSettlementInq();

            //Terminal
            ret.Terminal = new GVPSTerminal();
            //ret.Terminal.HashData = formDataDic.Get("");
            ret.Terminal.ID = formDataDic.Get("clientid");
            ret.Terminal.MerchantID = formDataDic.Get("terminalmerchantid");
            ret.Terminal.ProvUserID = formDataDic.Get("terminalprovuserid");
            //TODO ret.Terminal.SubMerchantID = formDataDic.Get("");
            ret.Terminal.UserID = formDataDic.Get("terminaluserid");

            //Transaction
            ret.Transaction = new GVPSTransaction();
            ret.Transaction.Amount = ulong.Parse(formDataDic.Get("txnamount"));
            ret.Transaction.AuthCode = formDataDic.Get("cavv");
            //TODO ret.Transaction.BatchNum = formDataDic.Get("");
            ret.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Secure3D;
            ret.Transaction.CurrencyCode = formDataDic.Get("txncurrencycode").GetValueFromXmlEnumName<GVPSCurrencyCodeEnum>();
            ret.Transaction.DelayDayCount = formDataDic.Get("txndelaydaycnt");
            ret.Transaction.DownPaymentRate = formDataDic.Get("txndownpayrate");
            ret.Transaction.InstallmentCnt = formDataDic.Get("txninstallmentcount");
            ret.Transaction.MotoInd = formDataDic.Get("txnmotoind").GetValueFromXmlEnumName<GVPSMotoIndEnum>();
            ret.Transaction.OriginalRetrefNum = formDataDic.Get("");
            //TODO ret.Transaction.ProvDate = formDataDic.Get("");
            //TODO ret.Transaction.RetrefNum = formDataDic.Get("");
            //TODO ret.Transaction.SequenceNum = formDataDic.Get("");
            ret.Transaction.Type = formDataDic.Get("txntype").GetValueFromXmlEnumName<GVPSTransactionTypeEnum>();

            //TODO Transaction CepBank
            //ret.Transaction.CepBank = new GVPSCepBank();
            //ret.Transaction.CepBank.GSMNumber = formDataDic.Get("");
            //ret.Transaction.CepBank.PaymentType = GVPSPaymentTypeEnum.Unspecified;

            //TODO Transaction CepBankInq
            //ret.Transaction.CepBankInq = new GVPSCepBankIng();
            //ret.Transaction.CepBankInq.GSMNumber = formDataDic.Get("");

            //TODO Transaction ChequeList
            //ret.Transaction.ChequeList = new GVPSChequeList();

            //TODO Transaction Commercial Card Extended Credit
            //ret.Transaction.CommercialCardExtendedCredit = new GVPSCommercialCardExtendedCredit();
            //ret.Transaction.CommercialCardExtendedCredit.PaymentList = new GVPSTransactionPaymentList();
            //var TransactionPaymentList = new List<GVPSTransactionPayment>();
            //TransactionPaymentList.Add(new GVPSTransactionPayment());
            //TransactionPaymentList.Last().Amount = formDataDic.Get("");
            //TransactionPaymentList.Last().DueDate = formDataDic.Get("");
            //TransactionPaymentList.Last().Number = ushort.Parse(formDataDic.Get(""));
            //ret.Transaction.CommercialCardExtendedCredit.PaymentList.Payment = TransactionPaymentList.ToArray();

            //TODO Transaction GSMUnitInq
            //ret.Transaction.GSMUnitInq = new GVPSGSMUnitInq();
            //ret.Transaction.GSMUnitInq.InstitutionCode = ushort.Parse(formDataDic.Get(""));
            //ret.Transaction.GSMUnitInq.Quantity = formDataDic.Get("gsmquantity");
            //ret.Transaction.GSMUnitInq.SubscriberCode = formDataDic.Get("utilitypaysubscode");
            //ret.Transaction.GSMUnitSales = new GVPSGSMUnitSales();

            //TODO Transaction GSMUnitSales
            //ret.Transaction.GSMUnitSales.InstitutionCode = ushort.Parse(formDataDic.Get(""));
            //ret.Transaction.GSMUnitSales.Quantity = formDataDic.Get("gsmquantity");
            //ret.Transaction.GSMUnitSales.SubscriberCode = formDataDic.Get("utilitypaysubscode");
            //ret.Transaction.GSMUnitSales.UnitID = formDataDic.Get("");

            //TODO Transaction HostMsgList
            ret.Transaction.HostMsgList = new GVPSHostMsgList();
            var HostMsgList = new List<string>();
            ret.Transaction.HostMsgList.HostMsg = HostMsgList.ToArray();

            //TODO Transaction MoneyCard
            //ret.Transaction.MoneyCard = new GVPSMoneyCard();

            //TODO Transaction Response
            //ret.Transaction.Response = new GVPSTransactionResponse();
            //ret.Transaction.Response.Code = formDataDic.Get("");
            //ret.Transaction.Response.ErrorMsg = formDataDic.Get("");
            //ret.Transaction.Response.Message = formDataDic.Get("");
            //ret.Transaction.Response.ReasonCode = formDataDic.Get("");
            //ret.Transaction.Response.Source = formDataDic.Get("");
            //ret.Transaction.Response.SysErrMsg = formDataDic.Get("");

            //TODO Transaction RewardInqResult 
            //ret.Transaction.RewardInqResult = new GVPSRewardInqResult();
            //ret.Transaction.RewardInqResult.ChequeList = new GVPSChequeList();
            
            //TODO ret.Transaction.RewardInqResult.ChequeList.

            //TODO Transaction RewardList 
            //ret.Transaction.RewardList = new GVPSRewardList();
            //ret.Transaction.RewardList.Reward = new GVPSReward[] { new GVPSReward() { } }; //TODO fill GVPSResponseReward properties

            //Transaction Secure3D 
            ret.Transaction.Secure3D = new GVPSSecure3D();
            ret.Transaction.Secure3D.AuthenticationCode = formDataDic.Get("secure3dauthenticationcode"); 
            ret.Transaction.Secure3D.Md = formDataDic.Get("mdstatus").GetValueFromXmlEnumName<GVPSMdStatusEnum>();
            ushort securityLevel;
            if (ushort.TryParse(formDataDic.Get("eci"), out securityLevel)) //secure3dsecuritylevel
                ret.Transaction.Secure3D.SecurityLevel = securityLevel;
            ret.Transaction.Secure3D.TxnID = formDataDic.Get("secure3dtxnid");
            ret.Transaction.Secure3D.TxnID = formDataDic.Get("xid");


            //TODO Transaction UtilityPayment
            //ret.Transaction.UtilityPayment = new GVPSUtilityPayment();
            //ret.Transaction.UtilityPayment.Amount = ulong.Parse(formDataDic.Get(""));
            //ret.Transaction.UtilityPayment.InstitutionCode = ushort.Parse(formDataDic.Get(""));
            //ret.Transaction.UtilityPayment.InvoiceID = formDataDic.Get("");
            //ret.Transaction.UtilityPayment.SubscriberCode = formDataDic.Get("");

            //TODO Transaction UtilityPaymentInq
            //ret.Transaction.UtilityPaymentInq = new GVPSUtilityPaymentInq();
            //ret.Transaction.UtilityPaymentInq.InstitutionCode = ushort.Parse(formDataDic.Get(""));
            //ret.Transaction.UtilityPaymentInq.SubscriberCode = formDataDic.Get("");

            //TODO Transaction UtilityPaymentVoidInq
            //ret.Transaction.UtilityPaymentVoidInq = new GVPSUtilityPaymentVoidInq();
            //ret.Transaction.UtilityPaymentVoidInq.InstitutionCode = ushort.Parse(formDataDic.Get(""));
            //ret.Transaction.UtilityPaymentVoidInq.SubscriberCode = formDataDic.Get("");

            //TODO Transaction Verification
            //ret.Transaction.Verification = new GVPSVerification();
            //ret.Transaction.Verification.Identity = formDataDic.Get("");


            //Checks

            var Secure3DRandom = formDataDic.Get("secure3drnd");
            var Secure3DHash = formDataDic.Get("secure3dhash");
            //var Secure3DHash = formDataDic.Get("hash");
            var Secure3DRecordKey = formDataDic.Get("secure3DRecordKey");
            var Secure3DHashParams = formDataDic.Get("hashparams");
            var Secure3DHashParamsVal = formDataDic.Get("hashparamsval");
            string Secure3DHashCalculated = null;
            if (string.IsNullOrWhiteSpace(Secure3DHash))
                throw new ArgumentNullException("secure3dhash", "Bank hash response is null or empty.");
            if ((!string.IsNullOrWhiteSpace(Secure3DHashParams)) && (!string.IsNullOrWhiteSpace(Secure3DHashParamsVal)))
            {
                var Secure3DHashParamsList = Secure3DHashParams.Split(':');
                var Secure3DHashText = "";
                foreach (var item in Secure3DHashParamsList)
                {
                    Secure3DHashText += formDataDic[item].FirstOrDefault() ?? "";
                }
                if (string.IsNullOrWhiteSpace(storeKeyFor3D))
                    throw new ArgumentNullException("storeKeyFor3D");
                Secure3DHashCalculated = GetSHA1(Secure3DHashText + storeKeyFor3D).ToUpper();
            }
            if (string.IsNullOrWhiteSpace(Secure3DHashCalculated))
                throw new ArgumentNullException("Secure3DHashCalculated", "Secure3DHashCalculated is null or empty.");
            if (Secure3DHashCalculated != Secure3DHash)
                throw new ArgumentException("Bank hash response and calculated hash not equal.");

            var Hash3D = GetSHA1(
                ret.Terminal.ID
                + ret.Order.OrderID
                + ret.Transaction.Amount.ToString()
                + successUri.ToString()
                + failUri.ToString()
                + ret.Transaction.Type
                + ret.Transaction.InstallmentCnt
                + storeKeyFor3D
                + this.hashedPassword
                ).ToUpper();

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        #region Privates
        private string isRequireZero(int time)
        {
            return time < 10 ? String.Format("0{0}", time) :
                                                            time > 2000 ?
                                                            isRequireZero(time - 2000) : time.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        static string isRequireZero(string id, int complete)
        {
            var _tmp = id.Trim();

            if (_tmp.Length < complete)
                for (int i = 0; (i < (complete - _tmp.Length)); i++)
                    id = id.Insert(0, "0");

            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SHA1Data"></param>
        /// <returns></returns>
        private string GetSHA1(string SHA1Data)
        {
            var sha = new SHA1CryptoServiceProvider();
            var HashedPassword = SHA1Data;

            byte[] hashbytes = UsingEncoding.GetBytes(HashedPassword);
            //byte[] hashbytes = Encoding.GetEncoding("iso-8859-9").GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);

            return GetHexaDecimal(inputbytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string GetHexaDecimal(byte[] bytes)
        {
            var s = new StringBuilder();
            var length = bytes.Length;

            for (int n = 0; n <= length - 1; n++)
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));

            return s.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TModel"></param>
        /// <returns></returns>
        private string SerializeObjectToXmlString<T>(T TModel)
        {
            string xmlData = String.Empty;

            XmlSerializerNamespaces EmptyNameSpace = new XmlSerializerNamespaces();
            EmptyNameSpace.Add("", "");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream, UsingEncoding);

            xmlSerializer.Serialize(xmlWriter, TModel, EmptyNameSpace);

            memoryStream = (MemoryStream)xmlWriter.BaseStream;
            //xmlData = UTF8ByteArrayToString(memoryStream.ToArray());
            xmlData = UsingEncoding.GetString(memoryStream.ToArray());

            return xmlData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private T DeSerializeObject<T>(string xmlData)
        {
            T deSerializeObject = default(T);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xmlData);
            XmlReader XR = new XmlTextReader(stringReader);

            if (xmlSerializer.CanDeserialize(XR))
            {
                deSerializeObject = (T)xmlSerializer.Deserialize(XR);
            }

            return deSerializeObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="Method"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        private string SendHttpRequest(string Host, string Method, string Params)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate
                                                                            (object sender, X509Certificate certificate, X509Chain chain,
                                                                            SslPolicyErrors sslPolicyErrors)
            { return true; };

            var returnSrting = String.Empty;

            var request = (HttpWebRequest)WebRequest.Create(Host);
            request.Timeout = 30000;
            request.Method = Method;

            var bytes = new ASCIIEncoding().GetBytes(Params);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Params.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                returnSrting = sr.ReadToEnd();
            }

            return returnSrting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Use3D"></param>
        /// <returns></returns>
        private GVPSResponse Send(bool Use3D = false)
        {
            var gvpResponse = new GVPSResponse();
            var xmlString = SerializeObjectToXmlString<GVPSRequest>(request);
            gvpResponse.RawRequest = xmlString;

            try
            {
                var responseString = string.Empty;
                if (Use3D)
                {
                    responseString = SendHttpRequest(REQUEST_URL_FOR_3D, "Post", String.Format("data={0}", WebUtility.UrlEncode(xmlString)));
                }
                else
                {
                    responseString = SendHttpRequest(REQUEST_URL, "Post", String.Format("data={0}", WebUtility.UrlEncode(xmlString)));
                }
                gvpResponse.RawResponse = responseString;
                gvpResponse = DeSerializeObject<GVPSResponse>(responseString);
                gvpResponse.RawRequest = xmlString;
                gvpResponse.RawResponse = responseString;
            }
            catch (Exception ex)
            {
                gvpResponse.Transaction = new GVPSTransaction();
                gvpResponse.Order = new GVPSOrder();
                gvpResponse.Transaction.Response = new GVPSTransactionResponse();

                gvpResponse.Transaction.Response.Code = "99";
                gvpResponse.Transaction.Response.Message = ex.Message;
                gvpResponse.Transaction.Response.ErrorMsg = ex.StackTrace;
            }

            return gvpResponse;
        }
        #endregion
    }    
}
;