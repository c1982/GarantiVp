using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType =true)]
    public class GVPSVerification
    {
        [XmlElement("Identity")]
        public string Identity { get; set; }
    }
}