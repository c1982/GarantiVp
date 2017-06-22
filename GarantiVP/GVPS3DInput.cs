using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType =true)]
    public class GVPS3DInput
    {
        [XmlAttribute]
        public string type { get; set; } = "hidden";

        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string value { get; set; }

    }
}