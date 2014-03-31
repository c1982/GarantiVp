namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSRequest", Namespace = null)]
    public class GVPSRequest
    {
        [XmlElement]
        public string Mode { get; set; }

        [XmlElement]
        public string Version { get; set; }

        [XmlElement]
        public Terminal Terminal { get; set; }

        [XmlElement]
        public Customer Customer { get; set; }

        [XmlElement]
        public Card Card { get; set; }

        [XmlElement]
        public Order Order { get; set; }

        [XmlElement]
        public Transaction Transaction { get; set; }


    }

    public class Terminal
    {
        [XmlElement]
        public string ProvUserID { get; set; }

        [XmlElement]
        public string HashData { get; set; }

        [XmlElement]
        public string UserID { get; set; }

        [XmlElement]
        public string ID { get; set; }

        [XmlElement]
        public string MerchantID { get; set; }

    }

    public class Customer
    {
        [XmlElement]
        public string IPAddress { get; set; }

        [XmlElement]
        public string EmailAddress { get; set; }
    }

    public class Card
    {

        [XmlElement]
        public string Number { get; set; }

        /// <summary>
        /// must be MMYY format.
        /// </summary>
        [XmlElement]
        public string ExpireDate { get; set; }

        [XmlElement]
        public string CVV2 { get; set; }

    }

    public class Order
    {
        [XmlElement]
        public string OrderID { get; set; }

        [XmlElement]
        public string GroupID { get; set; }
    }

    public class Transaction
    {
        [XmlElement]
        public string Type { get; set; }

        [XmlElement]
        public string InstallmentCnt { get; set; }

        [XmlElement]
        public string Amount { get; set; }

        [XmlElement]
        public string CurrencyCode { get; set; }

        [XmlElement]
        public string CardholderPresentCode { get; set; }

        [XmlElement]
        public string MotoInd { get; set; }

        [XmlElement]
        public string OriginalRetrefNum { get; set; }

        [XmlElement]
        public string DelayDayCount { get; set; }

        [XmlElement]
        public string DownPaymentRate { get; set; }

        [XmlElement]
        public Verification Verification { get; set; }
    }

    public class Verification
    {
        public string Identity { get; set; }
    }

    public enum CurrencyCode : int
    {
        TRL = 949,
        USD = 840,
        EURO = 978,
        GBP = 826,
        JPY = 392
    }

    public enum TransactionType
    {
        sales,
        @void,
        refund,
        preauth,
        postauth,
        identifyinq
    }
}
