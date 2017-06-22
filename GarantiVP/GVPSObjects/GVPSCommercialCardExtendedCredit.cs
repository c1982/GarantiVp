using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Trading card transaction information
    /// As much as the installment number, it should be installment information.
    /// <para lang="tr">Ortak kart işlemleri bilgisi</para> 
    /// <para lang="tr">Taksit sayısı kadar vade bilgisi olmalıdır.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSCommercialCardExtendedCredit
    {
        /// <summary>
        /// Installment information
        /// <para lang="tr">Taksit bilgileri</para> 
        /// </summary>
        [XmlElement("PaymentList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSTransactionPaymentList PaymentList { get; set; }
    }
}