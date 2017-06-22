using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Information required for invoice payment transactions
    /// <para lang="tr">Fatura ödeme işlemleri için gerekli bilgiler</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSUtilityPayment : GVPSTransactionInqBase 
    {
        /// <summary>
        /// Invoce ID
        /// <para>Size ? Byte alfanumeric</para> 
        /// <para lang="tr">Fatura numarası</para> 
        /// </summary>
        [XmlElement]
        public string InvoiceID { get; set; }

        /// <summary>
        /// Amount
        /// <para>Size 19 Byte numeric</para> 
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para lang="tr">Tutar</para> 
        /// </summary>
        [XmlElement]
        public ulong Amount { get; set; }
    }
}