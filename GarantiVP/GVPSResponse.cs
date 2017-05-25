namespace GarantiVP
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSResponse", Namespace = null)]
    public class GVPSResponse
    {
        [XmlElement("Order")]
        public GVPSResponseOrder Order { get; set; }

        [XmlElement(ElementName = "Transaction")]
        public GVPSResponseTransaction Transaction { get; set; }

        [XmlIgnore()]
        public string RawRequest { get; set; }

        [XmlIgnore()]
        public string RawResponse { get; set; }
    }
}
