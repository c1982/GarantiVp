using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO GVPSRequestRewardList
    [XmlType(AnonymousType = true)]
    public class GVPSRequestRewardList
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Reward", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequesReward[] Reward { get; set; }
    }
}