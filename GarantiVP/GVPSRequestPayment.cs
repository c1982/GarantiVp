using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Amount information for payment
    /// <para lang="tr">Ödeme için miktar bilgisi</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public abstract class GVPSRequestPaymentBase
    {
        #region For public use
        
        /// <summary>
        /// Amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para lang="tr">Tutar</para> 
        /// </summary>
        [XmlElement]
        public string Amount { get; set; }

        #endregion
    }


    /// <summary>
    /// Amount information for recurring payment
    /// <para lang="tr">Ödeme için miktar bilgisi</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestReccurringPayment : GVPSRequestPaymentBase
    {
        #region Just used for Reccurring

        /// <summary>
        /// Payment order number
        /// Beginning with 1, an increment is specified.
        /// <para lang="tr">
        /// </para>Ödeme sıra numarası
        /// 1 den başlayarak, bir artarak belirtilir.</summary>
        [XmlElement]
        public ushort PaymentNum { get; set; }

        #endregion
    }

    /// <summary>
    /// Amount information for transaction payment
    /// <para lang="tr">Ödeme için miktar bilgisi</para> 
    /// </summary>
    public class GVPSRequestTransactionPayment : GVPSRequestPaymentBase
    {
        #region Just used for Transaction

        /// <summary>
        /// Payment order number
        /// Beginning with 1, an increment is specified.
        /// <para>Size 2 Byte numeric</para> 
        /// <para lang="tr">
        /// </para>Ödeme sıra numarası
        /// 1 den başlayarak, bir artarak belirtilir.</summary>
        [XmlElement]
        public ushort Number { get; set; }

        /// <summary>
        /// Installment date
        /// <para>Size 8 Byte numeric, Format yyyyMMdd</para> 
        /// <para lang="tr">
        /// </para>Taksit tarihi</summary>
        [XmlElement]
        public string DueDate { get; set; }

        #endregion

    }

}