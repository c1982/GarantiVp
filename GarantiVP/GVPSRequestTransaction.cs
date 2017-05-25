using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSRequestTransaction
    {
        /// <summary>
        /// Transaction type
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSTransactionType Type { get; set; }

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
        public GVPSCurrencyCodeEnum CurrencyCode { get; set; }

        /// <summary>
        /// For normal operations value is 0. For 3D secure operations value is 13.
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSCardholderPresentCodeEnum CardholderPresentCode { get; set; }

        /// <summary>
        /// For ECommerce operations value is N. For Moto operations value is Y.
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSMotoIndEnum MotoInd { get; set; }

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
        public GVPSRequestVerification Verification { get; set; }

        //#region For response fields

        ///// <summary>
        ///// The field where the generated transaction number is returned
        ///// </summary>
        //[XmlElement]
        //public string RetrefNum { get; set; }


        ///// <summary>
        ///// The field where the confirmation code is returned.
        ///// </summary>
        //[XmlElement]
        //public string AuthCode { get; set; }


        ///// <summary>
        ///// The field where the transaction sequence number is returned.
        ///// </summary>
        //[XmlElement]
        //public string SequenceNum { get; set; }


        ///// <summary>
        ///// The area where the provision date is rotated.
        ///// </summary>
        //[XmlElement]
        //public string ProvDate { get; set; }

        //#endregion
    }
}