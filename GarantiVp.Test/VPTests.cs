namespace GarantiVp.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GarantiVP;
    using System.Diagnostics;

    [TestClass]
    public class VPTests
    {
        private readonly string terminalId = "30691297";
        private readonly string merchandId = "7000679";
        private readonly string Password = "123qweASD";

        private readonly string orderId = "cc5f63d899e64f39b996b1e9156a270e";
        private readonly string OrderRefNumber = "000014610000";

        private readonly string credit_card_number = "0000209000008010";
        private readonly string credit_card_cvv2 = "123";
        private readonly int credit_card_month = 2;
        private readonly int credit_card_year = 15;

        [TestMethod]
        public void SalesTest()
        {
            var _pay = new Client().Test().Company(terminalId, merchandId, "PROVAUT", Password)
                                            .Customer("apsrc@gmail.com", "192.168.0.1")
                                            .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                            .Order(Guid.NewGuid().ToString("N"))
                                            .Amount(95, CurrencyCode.TRL)
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
        public void Sales_USD_Test()
        {
            var _pay = new Client().Test()
                                        .Company(terminalId, merchandId, "PROVAUT", Password)
                                            .Customer("apsrc@gmail.com", "192.168.0.1")
                                            .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                            .Order(Guid.NewGuid().ToString("N"))
                                            .Amount(95, CurrencyCode.USD)
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
        public void SalesWithInstallmentTest()
        {
            var _pay = new Client().Test()
                            .Company(terminalId, merchandId, "PROVAUT", Password)
                                .Customer("apsrc@gmail.com", "192.168.0.1")
                                .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                .Order(Guid.NewGuid().ToString("N"))
                                .Amount(95)
                                .Installment(2)
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
        public void Delay_SalesTest()
        {
            var _pay = new Client().Test()
                                        .Company(terminalId, merchandId, "PROVAUT", Password)
                                            .Customer("apsrc@gmail.com", "192.168.0.1")
                                            .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                            .Order(Guid.NewGuid().ToString("N"))
                                            .Delay(10)
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
        public void DownPaymentRate_SalesTest()
        {
            var _pay = new Client().Test()
                                        .Company(terminalId, merchandId, "PROVAUT", Password)
                                            .Customer("apsrc@gmail.com", "192.168.0.1")
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
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVRFN", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .Order(orderId)
                                    .Amount(95)
                                    .Cancel(OrderRefNumber);

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void RefundTest()
        {
            var _pay = new Client()
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVAUT", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .Order(orderId)
                                    .Amount(95)
                                    .Refund();

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void RefundCancelTest()
        {
            var _pay = new Client()
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVAUT", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .Order(orderId)
                                    .Amount(95)
                                    .RefundCancel(OrderRefNumber);

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void PreauthSalesTest()
        {
            var _pay = new Client()
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVAUT", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .Amount(95)
                                    .Preauth();

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void PostauthSalesTest()
        {
            var _pay = new Client()
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVAUT", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Order(orderId)
                                    .Amount(95)
                                    .Postauth(OrderRefNumber);

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void PostauthSalesCancelTest()
        {
            var _pay = new Client()
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVAUT", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .Order(orderId)
                                    .Amount(95)
                                    .PostauthCancel(OrderRefNumber);

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }

        [TestMethod]
        public void TCKNVerificationTest()
        {
            var tc_kimlik_no = "000000000000";

            var _pay = new Client()
                                    .Test()
                                    .Company(terminalId, merchandId, "PROVAUT", Password)
                                    .Customer("apsrc@gmail.com", "192.168.0.1")
                                    .Order(Guid.NewGuid().ToString("N"))
                                    .CreditCard(credit_card_number, credit_card_cvv2, credit_card_month, credit_card_year)
                                    .Amount(1)
                                    .Verification(tc_kimlik_no);

            Debug.WriteLine("Request: " + _pay.RawRequest);
            Debug.WriteLine("Response: " + _pay.RawResponse);

            Debug.WriteLine(_pay.Transaction.Response.Message);
            Debug.WriteLine(_pay.Transaction.Response.ErrorMsg);
            Debug.WriteLine(_pay.Transaction.Response.SysErrMsg);

            Assert.AreEqual("00", _pay.Transaction.Response.Code);
            Assert.AreEqual("Approved", _pay.Transaction.Response.Message);
        }
    }
}
