namespace GarantiVP
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Response packed
    /// <para lang="tr">Cevap paketi</para> 
    /// </summary>
    [Serializable]
    [XmlRoot("GVPSResponse", Namespace = null)]
    [XmlInclude(typeof(GVPSPacked))]
    public class GVPSResponse : GVPSPacked
    {
    }
}
