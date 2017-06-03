using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Corporate payment inquiry
    /// <para lang="tr">Kurum ödeme sorgulaması</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestUtilityPaymentInq : GVPSRequestTransactionInqBase
    {
    }
}