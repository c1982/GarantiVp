using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Amount information for variable recurrent payment
    /// <para lang="tr">Değişken tekrarlayan ödeme için tutar bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public abstract class GVPSPaymentListBase<T> where T : GVPSPaymentBase
    {
        /// <summary>
        /// Amount information for payment
        /// <para lang="tr">Ödeme için miktar bilgisi</para> 
        /// </summary>
        [XmlElement("Payment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public T[] Payment { get; set; }
    }
}