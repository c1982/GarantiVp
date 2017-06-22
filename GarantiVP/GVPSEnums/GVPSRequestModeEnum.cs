using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSRequestModeEnum
    {

        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Test
        /// </summary>
        [XmlEnum("TEST")]
        Test,

        /// <summary>
        /// Production
        /// <para lang="tr">Gerçek/Canlı/Üretim</para> 
        /// </summary>
        [XmlEnum("PROD")]
        Production
    }
}