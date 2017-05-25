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

            request.Version = "v0.01";

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

        public IGarantiVPBuilder AddOrderItem(uint number, string productCode, string productId, double prince, uint quantity, string description = null, double totalAmount = 0.0)
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
            totalAmount = (totalAmount == 0.0) ? (prince * quantity) : totalAmount;
            var item = new GVPSRequestItem();
            item.Description = description;
            item.Number = number;
            item.Prince = (ulong)(Math.Round(prince, 2) * 100);
            item.ProductCode = productCode ;
            item.ProductID = productId;
            item.Quantity = quantity;
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
                throw new ArgumentException("Text field must be max 20 char.");
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
            request.Transaction.CardholderPresentCode = GVPSCardholderPresentCodeEnum.Normal ;
            request.Transaction.OriginalRetrefNum = RetrefNum;

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