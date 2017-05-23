using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSRequestModeEnum
    {
        [XmlEnum("")]
        Unspecified,

        [XmlEnum("TEST")]
        Test,

        [XmlEnum("PROD")]
        Production
    }
}