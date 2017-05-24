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
        public GVPSRequestModeEnum Mode { get; set; }

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

    /// <summary>
    /// Terminal information
    /// </summary>
    public class Terminal
    {
        /// <summary>
        /// Provisional user code for the terminal.
        /// <para>Size 32 Byte</para>
        /// <para>PROVAUT is used as a provisioning user.</para>
        /// <para>PROVRFN is the user used for cancellation and return processes.</para>
        /// </summary>
        [XmlElement]
        public string ProvUserID { get; set; }

        [XmlElement]
        public string HashData { get; set; }

        /// <summary>
        /// The user performing the transaction is the user.
        /// <para>Size 32 Byte</para>
        /// <para>In the Call Center and similar channels, the code of the salesperson performing the sales transaction is written in this field.</para>
        /// </summary>
        [XmlElement]
        public string UserID { get; set; }

        /// <summary>
        /// Terminal ID
        /// <para>Size 9 Byte </para>
        /// </summary>
        [XmlElement]
        public string ID { get; set; }

        /// <summary>
        /// Merchant reference
        /// </summary>
        [XmlElement]
        public string MerchantID { get; set; }

        /// <summary>
        /// Sub merchant reference
        /// </summary>
        public string SubMerchantID { get; set; }

    }

    /// <summary>
    /// Customer information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class Customer
    {
        [XmlElement]
        public string IPAddress { get; set; }

        [XmlElement]
        public string EmailAddress { get; set; }
    }

    /// <summary>
    /// Card information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class Card
    {
        /// <summary>
        /// Card number
        /// <para>Size Min:15, Max:19 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Number { get; set; }

        /// <summary>
        /// Card expire date. Must be MMYY format.
        /// <para>Size 4 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ExpireDate { get; set; }

        /// <summary>
        /// Card CVV number.
        /// <para>Size Min:3, Max:4 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CVV2 { get; set; }

    }

    /// <summary>
    /// Order information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class Order
    {
        /// <summary>
        /// Unique order reference.
        /// It is automatically generate by the bank when it is not sent.
        /// <para>Size 36 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrderID { get; set; }

        /// <summary>
        /// Used for reporting.
        /// <para>Size 36 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GroupID { get; set; }

        /// <summary>
        /// Contain order items.
        /// If you want to see in the virtual pos screens can be filled.
        /// </summary>
        [XmlElement("ItemList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestOrderItemList ItemList { get; set; }

        /// <summary>
        /// Contain order addresses.
        /// <para>Used for reporting. If you want to see in the virtual pos screens can be filled.</para>
        /// </summary>
        [XmlElement("AddressList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestOrderAddressList AddressList { get; set; }

        /// <summary>
        /// For special field definations.
        /// <para>Used for reporting. If you want to see in the virtual pos screens can be filled.</para>
        /// <para>But the virtual pos must be sent in the defined and defined order in the screens.</para>
        /// </summary>
        [XmlElement("CommentList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestOrderCommentList CommentList { get; set; }

        //TODO Recurring
        //TODO StartDate
        //TODO EndDate
        //TODO ListPageNum
    }

    [XmlType(AnonymousType =true)]
    public class Transaction
    {
        /// <summary>
        /// Transaction type
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TransactionType Type { get; set; }

        /// <summary>
        /// Number of installments. If it is sent empty, no installment will be made.
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InstallmentCnt { get; set; }

        /// <summary>
        /// Amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ulong Amount { get; set; }

        /// <summary>
        /// Currency code
        /// <para>Size 3 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public CurrencyCode CurrencyCode { get; set; }

        /// <summary>
        /// For normal operations value is 0. For 3D secure operations value is 13.
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestCardholderPresentCodeEnum CardholderPresentCode { get; set; }

        /// <summary>
        /// For ECommerce operations value is N. For Moto operations value is Y.
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestTransactionNotoIndEnum MotoInd { get; set; }

        /// <summary>
        /// Cancel - number generated for call
        /// <para>Size 12 Byte</para>
        /// </summary>
        [XmlElement]
        public string OriginalRetrefNum { get; set; }

        /// <summary>
        /// Number of days shifted
        /// <para>Size 2 Byte numeric</para>
        /// </summary>
        [XmlElement]
        public string DelayDayCount { get; set; }

        /// <summary>
        /// Down payment rate
        /// <para>Size 2 Byte numeric</para>
        /// </summary>
        [XmlElement]
        public string DownPaymentRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Verification Verification { get; set; }

        #region For response fields

        /// <summary>
        /// The field where the generated transaction number is returned
        /// </summary>
        [XmlElement]
        public string RetrefNum { get; set; }


        /// <summary>
        /// The field where the confirmation code is returned.
        /// </summary>
        [XmlElement]
        public string AuthCode { get; set; }


        /// <summary>
        /// The field where the transaction sequence number is returned.
        /// </summary>
        [XmlElement]
        public string SequenceNum { get; set; }


        /// <summary>
        /// The area where the provision date is rotated.
        /// </summary>
        [XmlElement]
        public string ProvDate { get; set; }

        #endregion
    }

    public class Verification
    {
        public string Identity { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public enum CurrencyCode : int
    {
        [XmlEnum("")]
        Unspecified,

        [XmlEnum("949")]
        TRL = 949,

        [XmlEnum("840")]
        USD = 840,

        [XmlEnum("978")]
        EURO = 978,

        [XmlEnum("826")]
        GBP = 826,

        [XmlEnum("392")]
        JPY = 392
    }

    [XmlType(AnonymousType = true)]
    public enum TransactionType
    {
        [XmlEnum("")]
        Unspecified,

        [XmlEnum("sales")]
        sales,

        [XmlEnum("@void")]
        @void,

        [XmlEnum("refund")]
        refund,

        [XmlEnum("preauth")]
        preauth,

        [XmlEnum("postauth")]
        postauth,

        [XmlEnum("identifyinq")]
        identifyinq
    }
}
