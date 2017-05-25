namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSRequest", Namespace = null)]
    public class GVPSRequest
    {
        [XmlElement]
        public GVPSRequestModeEnum Mode { get; set; }

        [XmlElement]
        public string Version { get; set; }

        [XmlElement]
        public GVPSRequestTerminal Terminal { get; set; }

        [XmlElement]
        public GVPSRequestCustomer Customer { get; set; }

        [XmlElement]
        public GVPSRequestCard Card { get; set; }

        [XmlElement]
        public GVPSRequestOrder Order { get; set; }

        [XmlElement]
        public GVPSRequestTransaction Transaction { get; set; }


    }

}
