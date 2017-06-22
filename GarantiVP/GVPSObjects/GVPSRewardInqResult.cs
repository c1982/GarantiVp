using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSRewardInqResult
    {
        [XmlElement("RewardList")]
        public GVPSRewardList RewardList { get; set; }

        [XmlElement("ChequeList")]
        public GVPSChequeList ChequeList { get; set; }
    }
}