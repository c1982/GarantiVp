namespace GarantiVp.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GarantiVP;
    using System.Diagnostics;
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    [TestClass]
    public class VPTests
    {
        //GARANTI VPos definations
        private const string MerchandID = "7000679";
        private const string SubMerchandID = null;
        //private const string MerchandID = "30690168";
        //private const string SubMerchandID = "35204";
        private const string SecureKey = "1234578";

        //GARANTI Terminal definations
        private const string TerminalID_For_XML = "30691297";
        private const string TerminalID_For_3D = "30691297";
        private const string TerminalID_For_3D_PAY = "30691298";
        private const string TerminalID_For_3D_OOS_PAY = "30691299";
        private const string TerminalID_For_OOS_PAY = "30691300";
        private const string TerminalID_For_3D_FULL = "30691301";

        //GARANTI Provision user definations
        private const string ProvUserID_AUT = "PROVAUT";
        private const string ProvUserID_FRN = "PROVRFN";
        private const string ProvUserID_OOS = "PROVOOS";
        private const string ProvUserID_3DS = "GARANTI";
        private const string ProvUserPassword = "123qweASD";

        //3D Secure configs
        private Uri HostUri = new Uri("http://localhost:5000");
        private const string HostUriSuccessPath = "/success";
        private const string HostUriFailPath = "/fail";

        //Credit card details
        private const string credit_card_number = "4282209027132016";
        private const string credit_card_cvv2 = "358";
        private const int credit_card_month = 5;
        private const int credit_card_year = 18;

        //Credit card details for 3D
        private const string credit_card_number_for_3D = "4282209004348015";
        private const string credit_card_cvv2_for_3D = "123";
        private const int credit_card_month_for_3D = 7;
        private const int credit_card_year_for_3D = 19;

        //Order details
        static string OrderId = "cc5f63d899e64f39b996b1e9156a270e";
        static string OrderIdForCancel; //Getting SalesTest
        static string OrderRefNumberForCancel; //Getting SalesTest
        static string OrderIdForRefund; //Getting SalesWithDetailsTest
        static string OrderIdForRefundCancel; //Getting RefundTest
        static string OrderRefNumberForRefundCancel; //Getting RefundTest
        static string OrderIdForPreAuthSales; //Getting PreAuthSales
        static string OrderRefNumberForPreaAuthSales; //Getting PreAuthSales
        static bool PreAuthCancelTestSuccess;

        //Order address details
        private const string order_address_name = "TÜBİTAK";
        private const string order_address_city = "Ankara";
        private const string order_address_district = "Çankaya";
        private const string order_address_text = "Remzi Oğuz Arık Mah., Tunus Cd. No:80";
        private const string order_address_postalCode = "06540";
        private const string order_address_phone = "+903124685300";

        //Customer details
        private const string customer_email = "eticaret@garanti.com.tr";
        private const string customer_ipAddress = "192.168.0.1";

        private void ValidateResult(GVPSResponse result)
        {
            Assert.IsNotNull(result);
            if(result != null)
            {
                Debug.WriteLine("Request: " + result.RawRequest);
                Debug.WriteLine("Response: " + result.RawResponse);

                Debug.WriteLine("Message: " + result.Transaction.Response.Message);
                Debug.WriteLine("ErrorMsg: " + result.Transaction.Response.ErrorMsg);
                Debug.WriteLine("SysErrMsg: " + result.Transaction.Response.SysErrMsg);

                Debug.WriteLine("OrderID: " + ((result.Order == null) ? "" : result.Order.OrderID));
                Debug.WriteLine("RetrefNum: " + result.Transaction.RetrefNum);
                Debug.WriteLine("AuthCode: " + result.Transaction.AuthCode);
                Debug.WriteLine("SequenceNum: " + result.Transaction.SequenceNum);
                Debug.WriteLine("BatchNum: " + result.Transaction.BatchNum);
                if ((result.Transaction.HostMsgList != null) && (result.Transaction.HostMsgList.HostMsg != null))
                {
                    foreach (string item in result.Transaction.HostMsgList.HostMsg)
                    {
                        Debug.WriteLine("Bank message: " + item);
                    }
                }
                var ProvDate = result.Transaction.ProvDate;
                object ProvisionDate = null;
                if (!string.IsNullOrWhiteSpace(ProvDate))
                {
                    ProvisionDate = new DateTime(int.Parse(ProvDate.Substring(0, 4)), int.Parse(ProvDate.Substring(4, 2)), int.Parse(ProvDate.Substring(6, 2)), int.Parse(ProvDate.Substring(9, 2)), int.Parse(ProvDate.Substring(12, 2)), int.Parse(ProvDate.Substring(15, 2)));
                }
                Debug.WriteLine("Provision date: " + ProvisionDate);
                if (result.Transaction.RewardInqResult != null)
                {
                    if (result.Transaction.RewardInqResult.ChequeList != null)
                    {
                        //TODO _pay.Transaction.RewardInqResult.ChequeList childs
                    }
                    if ((result.Transaction.RewardInqResult.RewardList != null) && (result.Transaction.RewardInqResult.RewardList.Reward != null))
                    {
                        foreach (GVPSReward item in result.Transaction.RewardInqResult.RewardList.Reward)
                        {
                            Debug.WriteLine("Reward\n Type: {0}\n Used: {1}\n Earned:{2}", item.Type, (item.TotalAmount / 100), (item.LastTxnGainAmount / 100));
                        }
                    }

                }

                Assert.AreEqual("00", result.Transaction.Response.Code);
                Assert.AreEqual("Approved", result.Transaction.Response.Message);
            }
        }

        private string GenerateMD5(string val)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var md5Bytes = System.Text.Encoding.UTF8.GetBytes(val);
            var md5Hash = md5.ComputeHash(md5Bytes, 0, md5Bytes.Length);
            var ret = BitConverter.ToString(md5Hash).Replace("-", "");
            return ret;
        }

        [TestInitialize()]
        private void Initial()
        {
            OrderId = Guid.NewGuid().ToString("N"); //GenerateMD5(DateTime.Now.ToString());
            OrderIdForCancel = OrderId;
        }
        
        [TestMethod]
        public void SalesTest()
        {
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword, SubMerchandID)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(1234.567, GVPSCurrencyCodeEnum.TRL)
                                    .Sales();
            ValidateResult(_pay);
            OrderIdForCancel = _pay.Order.OrderID;
            OrderRefNumberForCancel = _pay.Transaction.RetrefNum;
        }

        [TestMethod]
        public void SalesWithDetailsTest()
        {
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword, SubMerchandID)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .AddOrderAddress(GVPSAddressTypeEnum.Billing, order_address_city, order_address_district, order_address_text , order_address_phone, null, null, order_address_name, order_address_postalCode)
                                    .AddOrderItem(1, "0001", "ProductA ğüşiöçĞÜŞİÖÇ", 1.456, 3.456, "product A ğüşiöçĞÜŞİÖÇ description")
                                    .AddOrderItem(2, "0002", "ProductB", 1.4, 1.1, "product B description")
                                    .AddOrderComment(1, "COM1 ğüşiöçĞÜŞİÖÇ")
                                    .Amount(95, GVPSCurrencyCodeEnum.TRL)
                                    .Sales();
            ValidateResult(_pay);
            OrderIdForRefund = _pay.Order.OrderID;
        }

        [TestMethod]
        public void Sales_USD_Test()
        {
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95, GVPSCurrencyCodeEnum.USD)
                                    .Sales();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void SalesWithInstallmentTest()
        {
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95)
                                    .Installment(2)
                                    .Sales();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void Delay_SalesTest()
        {
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Delay(10)
                                    .Amount(95)
                                    .Sales();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void DownPaymentRate_SalesTest()
        {
            var _pay = new GVPSClient().Test(true)
                                        .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                            .Customer(customer_email, customer_ipAddress)
                                            .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                            .Order(Guid.NewGuid().ToString("N"))
                                            .DownPaymentRate(5)
                                            .Amount(95)
                                            .Sales();

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void CancelTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (string.IsNullOrWhiteSpace(OrderIdForCancel) && (sw.ElapsedMilliseconds < 10000))
            {
                System.Threading.Thread.SpinWait(1000);
            }
            sw.Stop();
            sw = null;
            if (string.IsNullOrWhiteSpace(OrderIdForCancel))
                throw new ArgumentNullException("orderIdForCancel");
            if (string.IsNullOrWhiteSpace(OrderIdForCancel))
                throw new ArgumentNullException("OrderRefNumberForCancel");
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_FRN, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(OrderIdForCancel)
                                    .Amount(95)
                                    .Cancel(OrderRefNumberForCancel);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void RefundTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (string.IsNullOrWhiteSpace(OrderIdForRefund) && (sw.ElapsedMilliseconds < 10000))
            {
                System.Threading.Thread.SpinWait(1000);
            }
            sw.Stop();
            sw = null;
            if (string.IsNullOrWhiteSpace(OrderIdForRefund))
                throw new ArgumentNullException("orderIdForRefund");
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(OrderIdForRefund)
                                    .Amount(95)
                                    .Refund();

            ValidateResult(_pay);
            OrderIdForRefundCancel = _pay.Order.OrderID;
            OrderRefNumberForRefundCancel = _pay.Transaction.RetrefNum;
        }

        [TestMethod]
        public void RefundCancelTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (string.IsNullOrWhiteSpace(OrderRefNumberForRefundCancel) && (sw.ElapsedMilliseconds < 10000))
            {
                System.Threading.Thread.SpinWait(1000);
            }
            sw.Stop();
            sw = null;
            if (string.IsNullOrWhiteSpace(OrderRefNumberForRefundCancel))
                throw new ArgumentNullException("OrderRefNumberForRefundCancel");
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(OrderIdForRefundCancel)
                                    .Amount(95)
                                    .RefundCancel(OrderRefNumberForRefundCancel);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void PreauthSalesTest()
        {
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95)
                                    .Preauth();

            ValidateResult(_pay);
            OrderIdForPreAuthSales = _pay.Order.OrderID;
            OrderRefNumberForPreaAuthSales = _pay.Transaction.RetrefNum;
        }

        [TestMethod]
        public void PostauthSalesTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            while ((!PreAuthCancelTestSuccess) && (sw.ElapsedMilliseconds < 10000))
            {
                System.Threading.Thread.SpinWait(1000);
            }
            sw.Stop();
            sw = null;
            OrderIdForPreAuthSales = null;
            OrderRefNumberForPreaAuthSales = null;
            PreauthSalesTest();
            if (string.IsNullOrWhiteSpace(OrderIdForPreAuthSales))
                throw new ArgumentNullException("OrderIdForPreAuthSales");
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(OrderIdForPreAuthSales)
                                    .Amount(95)
                                    .Postauth(OrderRefNumberForPreaAuthSales);

            ValidateResult(_pay);

        }

        [TestMethod]
        public void PostauthSalesCancelTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (string.IsNullOrWhiteSpace(OrderIdForPreAuthSales))
            {
                System.Threading.Thread.SpinWait(1000);
            }
            sw.Stop();
            sw = null;
            OrderIdForPreAuthSales = null;
            OrderRefNumberForPreaAuthSales = null;
            PreauthSalesTest();
            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(OrderIdForPreAuthSales)
                                    .Amount(95)
                                    .PostauthCancel(OrderRefNumberForPreaAuthSales);

            ValidateResult(_pay);
            PreAuthCancelTestSuccess = true;
        }

        [TestMethod]
        public void TCKNVerificationTest()
        {
            var tc_kimlik_no = "000000000000";

            var _pay = new GVPSClient()
                                    .Test(true)
                                    .Company(TerminalID_For_XML, MerchandID, ProvUserID_AUT, ProvUserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Amount(1)
                                    .Verification(tc_kimlik_no);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void Sale3DTest()
        {
            var IsFail = true;
            string FailMessage = null;
            var VPClient = new GVPSClient();
            Uri successUri = new Uri(HostUri, HostUriSuccessPath);
            Uri failUri = new Uri(HostUri, HostUriFailPath);
            var Request = VPClient
                        .Test(true)
                        .Company(TerminalID_For_3D_FULL, MerchandID, ProvUserID_3DS, ProvUserPassword, SubMerchandID)
                        .Customer(customer_email, customer_ipAddress)
                        .CreditCard(credit_card_number_for_3D, credit_card_cvv2_for_3D, credit_card_month_for_3D, credit_card_year_for_3D)
                        .Order("OID" + DateTime.Now.Ticks.ToString())
                        //.AddOrderAddress(GVPSAddressTypeEnum.Billing, order_address_city, order_address_district, order_address_text, order_address_phone, null, null, order_address_name, order_address_postalCode)
                        //.AddOrderAddress(GVPSAddressTypeEnum.Shipping, order_address_city, order_address_district, order_address_text, order_address_phone, null, null, order_address_name, order_address_postalCode)
                        //.AddOrderItem(1, "0001", "ProductA ğüşiöçĞÜŞİÖÇ", 1.456, 3.456, "product A ğüşiöçĞÜŞİÖÇ description")
                        //.AddOrderItem(2, "0002", "ProductB", 1.4, 1.1, "product B description")
                        //.AddOrderComment(1, "COM1 ğüşiöçĞÜŞİÖÇ")
                        .Amount(95, GVPSCurrencyCodeEnum.TRL)
                        .Sale3DRequest(SecureKey, successUri, failUri);
            Request.AddInput("", "Gönder", "submit");
            var HTML = Request.OuterXml;
            Debug.WriteLine("Secure 3D Form: " + HTML);
            var i = SelfHost.Run(HostUri.ToString())
                .Listen("/Sales3DTest", (IOwinContext con) => {
                    Debug.WriteLine("REQUEST Sales3DTest");
                    con.Response.Expires = DateTimeOffset.Now.AddDays(-1);
                    string strRegex = @"<input type=""hidden"" name=""(\w*)"" value=""([\w|\.|\:|\/|\@]*)"" />";
                    string strReplace = "\n" + @"<div class=""row"">" + "\n" + @"<div class=""col-md-2"">$1</div>" + "\n" + @"<div class=""col-md-2"">$2</div>" + "\n" + @"</div>" + "\n";
                    Regex myRegex = new Regex(strRegex, RegexOptions.None);
                    HTML = myRegex.Replace(HTML, strReplace).Replace(@"<input type=""submit"" name="""" value=""Gönder"" />", "").Replace("<form", "<div").Replace("</form>", "</div>") + HTML;
                    con.Response.Write(SelfHost.CreateWebContent(HTML, "Sales 3D test page"));
                })
                .Listen(HostUriSuccessPath, async (IOwinContext con) =>
                {
                    Debug.WriteLine("REQUEST " + HostUriSuccessPath);
                    con.Response.Expires = DateTimeOffset.Now.AddDays(-1);
                    var ResponseHTML = "";
                    try
                    {
                        var formData = await con.Request.ReadFormAsync() as IEnumerable<KeyValuePair<string, string[]>>;
                        ValidateResultFunction(con, formData, ref ResponseHTML, ref VPClient, successUri, failUri);
                        con.Response.Write(SelfHost.CreateWebContent(ResponseHTML, "Sales 3D SUCCESS page"));
                        IsFail = false;
                    }
                    catch (Exception exSuccess)
                    {
                        IsFail = true;
                        FailMessage = exSuccess.ToString();
                        con.Response.Write(SelfHost.CreateWebContent("<pre>" + exSuccess.ToString() + "</pre>" + ResponseHTML, "Sales 3D INTERNAL ERROR SUCCESS page"));
                        throw;
                    }
                })
                .Listen(HostUriFailPath, async (IOwinContext con) =>
                {
                    Debug.WriteLine("REQUEST " + HostUriFailPath);
                    IsFail = true;
                    var ResponseHTML = "";
                    try
                    {
                        var formData = (await con.Request.ReadFormAsync() as IEnumerable<KeyValuePair<string, string[]>>);
                        ValidateResultFunction(con, formData, ref ResponseHTML, ref VPClient, successUri, failUri);
                        con.Response.Write(SelfHost.CreateWebContent(ResponseHTML, "Sales 3D FAIL page"));
                    }
                    catch (Exception exFail)
                    {
                        FailMessage = exFail.ToString();
                        con.Response.Write(SelfHost.CreateWebContent("<pre>" + exFail.ToString() + "</pre>" + ResponseHTML, "Sales 3D INTERNAL ERROR FAIL page"));
                        throw;
                    }
                })
                //.OpenWebClient("/", false)
                .OpenWebClient("/Sales3DTest")
                ;
            Assert.IsFalse(IsFail, FailMessage);
            //OpenWebUI(HTML, "Sales3DTest");
            //var _pay = new GarantiVPClient()
            //            .Test(true)
            //            .Sales3DComplete(Request, Response);
            //ValidateResult(_pay);
            //OrderIdForCancel = _pay.Order.OrderID;
            //OrderRefNumberForCancel = _pay.Transaction.RetrefNum;
        }

        private void ValidateResultFunction(IOwinContext con, IEnumerable<KeyValuePair<string, string[]>> formData, ref string ret, ref GVPSClient VPClient, Uri successUri, Uri failUri)
        {
            //string ret = "";
            //var formData = con.Request.ReadFormAsync() as IEnumerable<KeyValuePair<string, string[]>>;
            var formDataDic = formData.ToDictionary(k => k.Key, e => e.Value);
            ret += string.Format("\n</br><strong>Method</strong>\n</br>{0}", con.Request.Method);
            Debug.WriteLine("INCOMING HEADERS");
            foreach (var item in con.Request.Headers)
            {
                Debug.WriteLine(string.Format("{0}={1}", item.Key, item.Value.FirstOrDefault()));
                ret += string.Format("\n</br><strong>Header {0}</strong> : {1}", item.Key, System.Net.WebUtility.HtmlEncode(string.Format("{0}", item.Value)));
            }
            if (con.Request.Method == "POST")
            {
                ret += SelfHost.CreateWebContent(formDataDic);
                var Result = VPClient.Sales3DEvaluatesResponseAndGetProvision(formDataDic, SecureKey, successUri, failUri);
                ValidateResult(Result);
            }
        }

        public string OpenWebUI(string HTMLContent, string PageTitle = "Local test content")
        {
            string ret = null;
            var wf = new System.Windows.Forms.Form();
            var wb = new System.Windows.Forms.WebBrowser();
            wb.Name = "wb";
            wb.Dock = System.Windows.Forms.DockStyle.Fill;
            wf.Controls.Add(wb);
            var HTML = "<!DOCTYPE>\n"
                + "<html>\n"
                + " <head>\n"
                + "     <title>" + System.Net.WebUtility.HtmlEncode(PageTitle) + "</title>\n"
                + "     <style type=\"text/css\" >\n"
                + "         *, html, body\n"
                + "         {\n"
                + "             margin:0;\n"
                + "             padding:0;\n"
                + "             font-family:Tahoma;\n"
                + "             font-size:10pt;\n"
                + "         }\n"
                + "     </style>\n"
                + " </head>\n"
                + " <body style=\"background:white;padding:20px;\">\n"
                + "         <h1 style=\"font-weight:normal;font-size:18pt;margin-top:5pt;border-bottom:1px solid gray;\">" + System.Net.WebUtility.HtmlEncode(PageTitle) + "</h1>\n"
                + "         " + HTMLContent + "\n"
                + " </body>\n"
                + "</html>\n";
            wf.Load += (object sender, EventArgs e) =>
            {
                wb.DocumentText = HTML;
                wf.Focus();
            };
            wf.ShowDialog();
            return ret;
        }

        private string ToOuterXML<T>(T TModel)
        {
            string xmlData = String.Empty;

            System.Xml.Serialization.XmlSerializerNamespaces EmptyNameSpace = new System.Xml.Serialization.XmlSerializerNamespaces();
            EmptyNameSpace.Add("", "");

            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var memoryStream = new System.IO.MemoryStream();
            var xmlWriter = new System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.Default);
            xmlSerializer.Serialize(xmlWriter, TModel, EmptyNameSpace);

            memoryStream = (System.IO.MemoryStream)xmlWriter.BaseStream;
            //xmlData = UTF8ByteArrayToString(memoryStream.ToArray());
            xmlData = System.Text.Encoding.Default.GetString(memoryStream.ToArray());

            return xmlData;
        }

    }
}
