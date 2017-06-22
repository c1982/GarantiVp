namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Request packed
    /// <para lang="tr">İstek paketi</para> 
    /// </summary>
    [Serializable]
    [XmlRoot("GVPSRequest", Namespace = null)]
    [XmlInclude(typeof(GVPSPacked))]
    public class GVPSRequest : GVPSPacked
    {
    }

}
