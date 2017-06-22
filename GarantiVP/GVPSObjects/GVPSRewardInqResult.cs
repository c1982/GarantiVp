using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRewardInqResult
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("RewardList")]
        public GVPSRewardList RewardList { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("ChequeList")]
        public GVPSChequeList ChequeList { get; set; }
    }
}