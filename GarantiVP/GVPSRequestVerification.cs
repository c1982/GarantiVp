using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType =true)]
    public class GVPSRequestVerification
    {
        [XmlElement("Identity")]
        public string Identity { get; set; }
    }
}