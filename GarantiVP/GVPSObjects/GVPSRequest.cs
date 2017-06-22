namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSRequest", Namespace = null)]
    [XmlInclude(typeof(GVPSPacked))]
    public class GVPSRequest : GVPSPacked
    {
    }

}
