using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSResponseTransaction
    {
        [XmlElement]
        public GVPSTransactionResponse Response { get; set; }

        [XmlElement]
        public string RetrefNum { get; set; }

        [XmlElement]
        public string AuthCode { get; set; }

        [XmlElement]
        public string BatchNum { get; set; }

        [XmlElement]
        public string SequenceNum { get; set; }

        [XmlElement]
        public string ProvDate { get; set; }

        [XmlElement("HostMsgList")]
        public GVPSResponseHostMsgList HostMsgList { get; set; }

        [XmlElement("RewardInqResult")]
        public GVPSResponseRewardInqResult RewardInqResult { get; set; }
    }
}