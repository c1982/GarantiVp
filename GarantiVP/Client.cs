namespace GarantiVP
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class Client : IGarantiVPBuilder
    {
        private GVPSRequest request;

        private readonly GVPSRequestModeEnum REQUEST_TEST_MODE = GVPSRequestModeEnum.Test;
        private readonly GVPSRequestModeEnum REQUEST_PROD_MODE =  GVPSRequestModeEnum.Production;
        private readonly string REQUEST_USER_PROVAUT = "PROVAUT"; //Provizyon kullanıcısı
        private readonly string REQUEST_USER_PROVRFN = "PROVRFN"; //İptal ve İade işlemlerinde kullanılır

        private string REQUEST_URL;
        private string _secureString;

        public Client(bool test = false)
        {
            request = new GVPSRequest();

            request.Mode = REQUEST_PROD_MODE;
            request.Version = "v0.01";

            request.Terminal = new Terminal();
            request.Terminal.ProvUserID = REQUEST_USER_PROVAUT;

            request.Transaction = new Transaction();
            request.Transaction.Type = TransactionType.sales;
            request.Transaction.MotoInd = GVPSRequestTransactionNotoIndEnum.ECommerce;
            request.Transaction.CurrencyCode = CurrencyCode.TRL;

            this.REQUEST_URL = "https://sanalposprov.garanti.com.tr/VPServlet"; ;
        }

        public IGarantiVPBuilder Server(string posUrl)
        {
            if (!String.IsNullOrEmpty(posUrl))
                REQUEST_URL = posUrl;

            return this;
        }

        public IGarantiVPBuilder Test(bool test = false)
        {
            if (test)
            {
                this.REQUEST_URL = "https://sanalposprovtest.garanti.com.tr/VPServlet";
                request.Mode = REQUEST_TEST_MODE;
            }

            return this;
        }


        public IGarantiVPBuilder Company(string terminalId, string MerchantID, string userID, string userPassword)
        {
            request.Terminal.ID = terminalId; //isRequireZero(terminalId, 9);
            request.Terminal.MerchantID = MerchantID;
            request.Terminal.UserID = userID;

            this._secureString = GetSHA1(userPassword + isRequireZero(terminalId, 9)).ToUpper();

            return this;
        }

        public IGarantiVPBuilder Customer(string customerMail, string customerIP)
        {
            request.Customer = request.Customer ?? new Customer();

            request.Customer.EmailAddress = customerMail;
            request.Customer.IPAddress = customerIP;

            return this;
        }

        public IGarantiVPBuilder CreditCard(string number, string cvv2, int month, int year)
        {
            request.Card = request.Card ?? new Card();

            request.Card.Number = number;
            request.Card.CVV2 = cvv2.ToString();
            request.Card.ExpireDate = String.Format("{0}{1}", isRequireZero(month), isRequireZero(year));

            return this;
        }

        public IGarantiVPBuilder Order(string orderID, string groupID = "")
        {
            request.Order = request.Order ?? new Order();
            request.Order.OrderID = orderID;
            request.Order.GroupID = groupID;

            return this;
        }

        public IGarantiVPBuilder Amount(double totalAmount, CurrencyCode currencyCode = CurrencyCode.TRL)
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
            request.Transaction.Type = TransactionType.sales;
            request.Transaction.CardholderPresentCode =  GVPSRequestCardholderPresentCodeEnum.Normal;

            request.Terminal.HashData = GetSHA1(request.Order.OrderID +
                                                    request.Terminal.ID +
                                                    request.Card.Number +
                                                    request.Transaction.Amount +
                                                    this._secureString).ToUpper();

            return Send();
        }

        public GVPSResponse Refund()
        {
            request.Transaction.Type = TransactionType.refund;
            request.Terminal.ProvUserID = REQUEST_USER_PROVRFN;
            request.Transaction.CardholderPresentCode = GVPSRequestCardholderPresentCodeEnum.Normal ;
            request.Transaction.MotoInd = GVPSRequestTransactionNotoIndEnum.H;

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
            request.Transaction.Type = TransactionType.@void;
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
            request.Transaction.Type = TransactionType.preauth;
            request.Transaction.MotoInd = GVPSRequestTransactionNotoIndEnum.ECommerce;
            request.Transaction.CardholderPresentCode = GVPSRequestCardholderPresentCodeEnum.Normal;

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
            request.Transaction.Type = TransactionType.postauth;
            request.Transaction.CardholderPresentCode = GVPSRequestCardholderPresentCodeEnum.Normal ;
            request.Transaction.OriginalRetrefNum = RetrefNum;

            return Send();
        }

        public GVPSResponse Verification(string TCKN)
        {
            request.Transaction.Verification = request.Transaction.Verification ?? new Verification();

            request.Transaction.Type = TransactionType.identifyinq;
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

            byte[] hashbytes = Encoding.GetEncoding("iso-8859-9").GetBytes(HashedPassword);
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
            XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream,
                Encoding.GetEncoding("iso-8859-9"));

            xmlSerializer.Serialize(xmlWriter, TModel, EmptyNameSpace);

            memoryStream = (MemoryStream)xmlWriter.BaseStream;
            xmlData = UTF8ByteArrayToString(memoryStream.ToArray());

            return xmlData;
        }

        private String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
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

        private GVPSResponse Send()
        {
            var gvpResponse = new GVPSResponse();
            var xmlString = SerializeObjectToXmlString<GVPSRequest>(request);

            gvpResponse.RawRequest = xmlString;

            try
            {
                var responseString = SendHttpRequest(REQUEST_URL, "Post", String.Format("data={0}", xmlString));
                gvpResponse.RawResponse = responseString;
                gvpResponse = DeSerializeObject<GVPSResponse>(responseString);
            }
            catch (Exception ex)
            {
                gvpResponse.Transaction = new ResponseTransaction();
                gvpResponse.Order = new ResponseOrder();
                gvpResponse.Transaction.Response = new Response();

                gvpResponse.Transaction.Response.Code = "99";
                gvpResponse.Transaction.Response.Message = ex.Message;
                gvpResponse.Transaction.Response.ErrorMsg = ex.StackTrace;
            }

            return gvpResponse;
        }
        #endregion
    }    
}
