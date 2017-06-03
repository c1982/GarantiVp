using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSResponseTransaction
    {
        [XmlElement]
        public GVPSResponseTransactionResponse Response { get; set; }

        /// <summary>
        /// Number generated for cancellation, refund and variable payment transactions
        /// <para lang="tr">İptal, geri ödeme ve değişken ödeme işlemleri için üretilen numara</para> 
        /// </summary>
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