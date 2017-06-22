using System;
using System.Xml.Serialization;

namespace GarantiVP
{
    [Serializable]
    [XmlRoot("form", Namespace = null)]
    public class GVPS3DForm
    {
        [XmlAttribute]
        public string method { get; set; } = "POST";

        [XmlAttribute]
        public string action { get; set; } = "https://sanalposprov.garanti.com.tr/servlet/gt3dengine";

        [XmlElement("input")]
        public GVPS3DInput[] Inputs { get; set; }
    }
}