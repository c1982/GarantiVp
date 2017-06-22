namespace GarantiVP
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSResponse", Namespace = null)]
    [XmlInclude(typeof(GVPSPacked))]
    public class GVPSResponse : GVPSPacked
    {
    }
}
