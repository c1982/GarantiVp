using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSCurrencyCodeEnum : int
    {
        [XmlEnum("")]
        Unspecified,

        [XmlEnum("949")]
        TRL = 949,

        [XmlEnum("840")]
        USD = 840,

        [XmlEnum("978")]
        EURO = 978,

        [XmlEnum("826")]
        GBP = 826,

        [XmlEnum("392")]
        JPY = 392
    }
}