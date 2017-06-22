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

    public class GarantiVPClient : IGarantiVPBuilder
    {
        private GVPSRequest request;

        private const GVPSRequestModeEnum REQUEST_TEST_MODE = GVPSRequestModeEnum.Test;
        private const GVPSRequestModeEnum REQUEST_PROD_MODE =  GVPSRequestModeEnum.Production;
        private const string REQUEST_USER_PROVAUT = "PROVAUT"; //Provizyon kullanıcısı
        private const string REQUEST_USER_PROVRFN = "PROVRFN"; //İptal ve İade işlemlerinde kullanılır

        private string REQUEST_URL;
        private string REQUEST_URL_FOR_3D;
        private string _secureString;
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

        public GarantiVPClient(bool test = false)
        {
            request = new GVPSRequest();
            var AsmName = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName();
            request.Version = AsmName.Name + " v" + AsmName.Version.Major.ToString() + "." + AsmName.Version.Minor.ToString();
            if(request.Version.Length > 16)
             request.Version = "v" + AsmName.Version.Major.ToString() + "." + AsmName.Version.Minor.ToString();

            request.Terminal = new GVPSRequestTerminal();
            request.Terminal.ProvUserID = REQUEST_USER_PROVAUT;

            request.Transaction = new GVPSRequestTransaction();
            request.Transaction.Type = GVPSTransactionType.sales;
            request.Transaction.MotoInd = GVPSMotoIndEnum.ECommerce;
            request.Transaction.CurrencyCode = GVPSCurrencyCodeEnum.TRL;

            this.Test(false);
        }

        public IGarantiVPBuilder Server(string posUrl)
        {
            if (!String.IsNullOrEmpty(posUrl))
                REQUEST_URL = posUrl;

            return this;
        }

        public IGarantiVPBuilder Test(bool IsTest)
        {
            if (IsTest)
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

        public IGarantiVPBuilder Company(string terminalId, string MerchantID, string userID, string userPassword, string SubMerchantID = null)
        {
            request.Terminal.ID = terminalId; //isRequireZero(terminalId, 9);
            request.Terminal.MerchantID = MerchantID;
            request.Terminal.SubMerchantID = SubMerchantID;
            request.Terminal.UserID = userID;

            this._secureString = GetSHA1(userPassword + isRequireZero(terminalId, 9)).ToUpper();

            return this;
        }

        public IGarantiVPBuilder Customer(string customerMail, string customerIP)
        {
            request.Customer = request.Customer ?? new GVPSRequestCustomer();

            request.Customer.EmailAddress = customerMail;
            request.Customer.IPAddress = customerIP;

            return this;
        }

        public IGarantiVPBuilder CreditCard(string number, string cvv2, int month, int year)
        {
            request.Card = request.Card ?? new GVPSRequestCard();

            request.Card.Number = number;
            request.Card.CVV2 = cvv2.ToString();
            request.Card.ExpireDate = String.Format("{0}{1}", isRequireZero(month), isRequireZero(year));

            return this;
        }

        public IGarantiVPBuilder Order(string orderID, string groupID = "")
        {
            request.Order = request.Order ?? new GVPSRequestOrder();
            request.Order.OrderID = orderID;
            request.Order.GroupID = groupID;
            return this;
        }

        public IGarantiVPBuilder AddOrderAddress(GVPSAddressTypeEnum type, string city, string district, string addressText, string phone, string name, string lastName, string Company = null, string postalCode = null)
        {
            var address = new GVPSRequestAddress();
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

        public IGarantiVPBuilder AddOrderAddress(GVPSRequestAddress address)
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
            request.Order.AddressList = request.Order.AddressList ?? new GVPSRequestAddressList();
            request.Order.AddressList.Address = request.Order.AddressList.Address ?? new GVPSRequestAddress[] { };
            var Addresses = new List<GVPSRequestAddress>();
            Addresses.AddRange(request.Order.AddressList.Address);
            if (Addresses.Where(e => e.Type.Equals(address.Type)).Count() > 0)
            {
                throw new ArgumentException("Address type already defined.");
            }

            Addresses.Add(address);
            request.Order.AddressList.Address = Addresses.ToArray();
            return this;
        }

        public IGarantiVPBuilder AddOrderItem(uint number, string productCode, string productId, double price, double quantity, string description = null, double totalAmount = 0.0)
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
            var item = new GVPSRequestItem();
            item.Description = description;
            item.Number = number;
            item.Price = (ulong)(Math.Round(price, 2) * 100);
            item.ProductCode = productCode ;
            item.ProductID = productId;
            item.Quantity = (ulong)(Math.Round(quantity, 2) * 100);
            item.TotalAmount = (ulong)(Math.Round(totalAmount, 2) * 100);
            return AddOrderItem(item);
        }

        public IGarantiVPBuilder AddOrderItem(GVPSRequestItem item)
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
            request.Order.ItemList = request.Order.ItemList ?? new GVPSRequestItemList();
            request.Order.ItemList.Item = request.Order.ItemList.Item ?? new GVPSRequestItem[] { };
            var Items = new List<GVPSRequestItem>();
            Items.AddRange(request.Order.ItemList.Item);
            Items.Add(item);
            request.Order.ItemList.Item = Items.ToArray();
            return this;
        }

        public IGarantiVPBuilder AddOrderComment(uint number, string text)
        {
            var comment = new GVPSRequestComment();
            comment.Number = number;
            comment.Text = text;
            return AddOrderComment(comment);
        }

        public IGarantiVPBuilder AddOrderComment(GVPSRequestComment comment)
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
            request.Order.CommentList = request.Order.CommentList ?? new GVPSRequestCommentList();
            request.Order.CommentList.Comment = request.Order.CommentList.Comment ?? new GVPSRequestComment[] { };
            var Comments = new List<GVPSRequestComment>();
            Comments.AddRange(request.Order.CommentList.Comment);
            if(Comments.Where(e => e.Number.Equals(comment.Number)).Count() > 0)
            {
                throw new ArgumentException("Comment number already defined.");
            }
            Comments.Add(comment);
            request.Order.CommentList.Comment = Comments.ToArray();
            return this;
        }

        public IGarantiVPBuilder Amount(double totalAmount, GVPSCurrencyCodeEnum currencyCode = GVPSCurrencyCodeEnum.TRL)
        {
            request.Transaction.Amount = (ulong)(Math.Round(totalAmount, 2) * 100);
            request.Transaction.CurrencyCode = currencyCode;

            return this;
        }

        public IGarantiVPBuilder Installment(int installment)
        {
            request.Transaction.InstallmentCnt = installment <= 0 ? string.Empty : installment.ToString();

            return this;
        }

        public IGarantiVPBuilder Delay(int day)
        {
            request.Transaction.DelayDayCount = day.ToString();

            return this;
        }

        public IGarantiVPBuilder DownPaymentRate(int rate)
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


            request.Transaction.Type = GVPSTransactionType.sales;
            request.Transaction.CardholderPresentCode =  GVPSCardholderPresentCodeEnum.Normal;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this._secureString).ToUpper();

            return Send();
        }

        public GVPSResponse Refund()
        {
            request.Transaction.Type = GVPSTransactionType.refund;
            request.Terminal.ProvUserID = REQUEST_USER_PROVRFN;
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal ;
            request.Transaction.MotoInd = GVPSMotoIndEnum.H;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                request.Terminal.ID +
                                                request.Transaction.Amount +
                                                this._secureString).ToUpper();

            return Send();
        }

        public GVPSResponse RefundCancel(string RefundRetrefNum)
        {
            return Cancel(RefundRetrefNum);
        }

        public GVPSResponse Cancel(string RetrefNum)
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
            request.Transaction.Type = GVPSTransactionType.@void;
            request.Terminal.ProvUserID = REQUEST_USER_PROVRFN;
            request.Transaction.OriginalRetrefNum = RetrefNum;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                request.Terminal.ID +
                                                request.Transaction.Amount +
                                                this._secureString).ToUpper();

            return Send();
        }

        public GVPSResponse Preauth()
        {
            request.Transaction.Type = GVPSTransactionType.preauth;
            request.Transaction.MotoInd = GVPSMotoIndEnum.ECommerce;
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal;
            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this._secureString).ToUpper();
            return Send();
        }

        public GVPSResponse PostauthCancel(string RetrefNum)
        {
            return Cancel(RetrefNum);
        }

        public GVPSResponse Postauth(string RetrefNum)
        {
            request.Transaction.Type = GVPSTransactionType.postauth;
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal;
            request.Transaction.OriginalRetrefNum = RetrefNum;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                        request.Terminal.ID +
                                        ((request.Card == null) ? "" : request.Card.Number) +
                                        request.Transaction.Amount +
                                        this._secureString).ToUpper();

            return Send();
        }

        public GVPSResponse Verification(string TCKN)
        {
            request.Transaction.Verification = request.Transaction.Verification ?? new GVPSRequestVerification();

            request.Transaction.Type = GVPSTransactionType.identifyinq;
            request.Transaction.Verification.Identity = TCKN;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this._secureString).ToUpper();

            return Send();
        }

        public XmlElement Sale3DRequest(string StoreKeyFor3D, Uri SuccessUri, Uri FailUri)
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


                request.Transaction.Type = GVPSTransactionType.sales;
                request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Secure3D;
                var OperationType = request.Transaction.Type.GetXmlEnumName();
                if (string.IsNullOrWhiteSpace(OperationType))
                    throw new ArgumentException("Transaction type not know; " + request.Transaction.Type.ToString());
                var Secure3DHash = GetSHA1(request.Terminal.ID
                                                    + request.Order.OrderID
                                                    + request.Transaction.Amount
                                                    + SuccessUri.ToString()
                                                    + FailUri.ToString()
                                                    + OperationType
                                                    + (request.Transaction.InstallmentCnt ?? "")
                                                    + StoreKeyFor3D
                                                    + this._secureString).ToUpper();

                string RefreshTime = null;
                string Lang = null;

                var xDoc = new XmlDocument();
                var xF = xDoc.CreateElement("form");
                xF.SetAttribute("method", "POST");
                xF.SetAttribute("action", REQUEST_URL_FOR_3D);

                //Root
                xF.AddInput("mode", request.Mode.GetXmlEnumName()); //Required
                xF.AddInput("version", request.Version); //Required
                xF.AddInput("secure3dhash", Secure3DHash); //Required
                xF.AddInput("refreshtime", RefreshTime); //Required
                xF.AddInput("lang", Lang); //Required

                //Terminal information
                xF.AddInput("terminalprovuserid", request.Terminal.ProvUserID);
                xF.AddInput("terminaluserid", request.Terminal.UserID); //Required
                xF.AddInput("terminalmerchantid", request.Terminal.MerchantID); //Required
                xF.AddInput("terminalid", request.Terminal.ID); //Required

                //Transaction information
                xF.AddInput("txntype", request.Transaction.Type.GetXmlEnumName()); //Required
                xF.AddInput("txnamount", request.Transaction.Amount.ToString()); //Required
                xF.AddInput("txncurrencycode", request.Transaction.CurrencyCode.GetXmlEnumName()); //Required
                xF.AddInput("txninstallmentcount", request.Transaction.InstallmentCnt);
                xF.AddInput("txndownpayrate", request.Transaction.DownPaymentRate);
                xF.AddInput("txndelaydaycnt", request.Transaction.DelayDayCount);
                xF.AddInput("txncardholderpresentcode", request.Transaction.CardholderPresentCode.GetXmlEnumName());
                xF.AddInput("txnmotoind", request.Transaction.MotoInd.GetXmlEnumName()); //Required

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
                xF.AddInput("successurl", SuccessUri.ToString());
                xF.AddInput("errorurl", FailUri.ToString());

                //Customer
                xF.AddInput("customeremailaddress", request.Customer.EmailAddress);
                xF.AddInput("customeripaddress", request.Customer.IPAddress.ToString());

                //Card
                xF.AddInput("Cardnumber", request.Card.Number); //Required
                xF.AddInput("cardcvv2", request.Card.CVV2); //Required
                xF.AddInput("cardexpiredatemonth", request.Card.ExpireDate.Substring(0, 2)); //Required
                xF.AddInput("cardexpiredateyear", request.Card.ExpireDate.Substring(2, 2)); //Required
                xF.AddInput("cardholder", request.Card.CardHolder);


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

 
        #region Privates
        private string isRequireZero(int time)
        {
            return time < 10 ? String.Format("0{0}", time) :
                                                            time > 2000 ?
                                                            isRequireZero(time - 2000) : time.ToString();
        }

        static string isRequireZero(string id, int complete)
        {
            var _tmp = id.Trim();

            if (_tmp.Length < complete)
                for (int i = 0; (i < (complete - _tmp.Length)); i++)
                    id = id.Insert(0, "0");

            return id;
        }

        private string GetSHA1(string SHA1Data)
        {
            var sha = new SHA1CryptoServiceProvider();
            var HashedPassword = SHA1Data;

            byte[] hashbytes = UsingEncoding.GetBytes(HashedPassword);
            //byte[] hashbytes = Encoding.GetEncoding("iso-8859-9").GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);

            return GetHexaDecimal(inputbytes);
        }

        private string GetHexaDecimal(byte[] bytes)
        {
            var s = new StringBuilder();
            var length = bytes.Length;

            for (int n = 0; n <= length - 1; n++)
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));

            return s.ToString();
        }

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
                gvpResponse.Transaction = new GVPSResponseTransaction();
                gvpResponse.Order = new GVPSResponseOrder();
                gvpResponse.Transaction.Response = new GVPSResponseTransactionResponse();

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