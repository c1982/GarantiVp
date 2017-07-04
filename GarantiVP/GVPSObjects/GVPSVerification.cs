using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSVerification
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Identity")]
        public string Identity { get; set; }
    }
}