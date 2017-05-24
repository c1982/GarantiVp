namespace GarantiVp.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GarantiVP;
    using System.Diagnostics;

    [TestClass]
    public class VPTests
    {
        //GARANTI VPos definations
        private const string MerchandID = "7000679";
        private const string SubMerchandID = "";
        private const string TerminalID_For_XML = "30691244";
        private const string TerminalID_For_3D = "30691297";
        private const string TerminalID_For_3D_PAY = "30691298";
        private const string TerminalID_For_3D_OOS_PAY = "30691299";
        private const string TerminalID_For_OOS_PAY = "30691300";
        private const string TerminalID_For_3D_FULL = "30691301";

        private const string ProvUserID_For_XML = "PROVAUT";
        private const string ProvUserID_For_3D = "GARANTI";
        private const string ProvUserPassword = "123qweASD";

        private const string Securekey = "12345678";

        //GARANTI VPos test configuration
        private string TerminalID = TerminalID_For_XML;
        private string UserID = ProvUserID_For_XML;
        private string UserPassword = ProvUserPassword;

        //Credit card details
        private const string credit_card_number = "4282209027132016";
        private const string credit_card_cvv2 = "232";
        private const int credit_card_month = 5;
        private const int credit_card_year = 15;

        //Credit card details for 3D
        private const string credit_card_number_for_3D = "4282209004348015";
        private const string credit_card_cvv2_for_3D = "123";
        private const int credit_card_month_for_3D = 2;
        private const int credit_card_year_for_3D = 15;

        //Order details
        private const string orderId = "cc5f63d899e64f39b996b1e9156a270e";
        private const string OrderRefNumber = "000014610000";

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

        private void ValidateResult(GVPSResponse _pay)
        {
            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void SalesTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, UserID, UserPassword, SubMerchandID)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95, CurrencyCode.TRL)
                                    .Sales();

            ValidateResult(_pay);
        }



        [TestMethod]
        public void SalesWithDetailsTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, UserID, UserPassword, SubMerchandID)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .AddOrderAddress(GVPSAddressTypeEnum.Billing, order_address_city, order_address_district, order_address_text , order_address_phone, null, null, order_address_name, order_address_postalCode)
                                    .AddOrderItem(1, "0001", "ProductA", 1.5, 3, "product description")
                                    .AddOrderComment(1, " COMMENT1 alanı açıklaması")
                                    .Amount(95, CurrencyCode.TRL)
                                    .Sales();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void Sales_USD_Test()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, UserID, UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95, CurrencyCode.USD)
                                    .Sales();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void SalesWithInstallmentTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
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
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
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
            var _pay = new Client().Test(true)
                                        .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
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
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVRFN", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(orderId)
                                    .Amount(95)
                                    .Cancel(OrderRefNumber);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void RefundTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(orderId)
                                    .Amount(95)
                                    .Refund();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void RefundCancelTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(orderId)
                                    .Amount(95)
                                    .RefundCancel(OrderRefNumber);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void PreauthSalesTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95)
                                    .Preauth();

            ValidateResult(_pay);
        }

        [TestMethod]
        public void PostauthSalesTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(orderId)
                                    .Amount(95)
                                    .Postauth(OrderRefNumber);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void PostauthSalesCancelTest()
        {
            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(orderId)
                                    .Amount(95)
                                    .PostauthCancel(OrderRefNumber);

            ValidateResult(_pay);
        }

        [TestMethod]
        public void TCKNVerificationTest()
        {
            var tc_kimlik_no = "000000000000";

            var _pay = new Client()
                                    .Test(true)
                                    .Company(TerminalID, MerchandID, "PROVAUT", UserPassword)
                                    .Customer(customer_email, customer_ipAddress)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Amount(1)
                                    .Verification(tc_kimlik_no);

            ValidateResult(_pay);
        }
    }
}
