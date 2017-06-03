using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Amount information for variable recurrent payment
    /// <para lang="tr">Değişken tekrarlayan ödeme için tutar bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public abstract class GVPSRequestPaymentListBase<T> where T : GVPSRequestPaymentBase
    {
        /// <summary>
        /// Amount information for payment
        /// <para lang="tr">Ödeme için miktar bilgisi</para> 
        /// </summary>
        [XmlElement("Payment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public T[] Payment { get; set; }
    }


    /// <summary>
    /// Amount information for variable recurrent payment
    /// <para lang="tr">Değişken tekrarlayan ödeme için tutar bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestReccuringPaymentList : GVPSRequestPaymentListBase<GVPSRequestReccurringPayment>
    {
    }

    /// <summary>
    /// Amount information for variable recurrent payment
    /// <para lang="tr">Değişken tekrarlayan ödeme için tutar bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestTransactionPaymentList : GVPSRequestPaymentListBase<GVPSRequestTransactionPayment>
    {
    }
}