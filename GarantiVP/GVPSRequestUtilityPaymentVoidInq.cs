using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Corporate payment cancellation inquiry
    /// <para lang="tr">Kurumsal ödeme iptali sorgusu</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestUtilityPaymentVoidInq : GVPSRequestTransactionInqBase 
    {
    }
}