using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSResponseRewardInqResult
    {
        [XmlElement("RewardList")]
        public GVPSResponseRewardList RewardList { get; set; }

        [XmlElement("ChequeList")]
        public GVPSResponseChequeList ChequeList { get; set; }
    }
}