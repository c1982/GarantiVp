using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSTransactionType
    {
        [XmlEnum("")]
        Unspecified,

        [XmlEnum("sales")]
        sales,

        [XmlEnum("@void")]
        @void,

        [XmlEnum("refund")]
        refund,

        [XmlEnum("preauth")]
        preauth,

        [XmlEnum("postauth")]
        postauth,

        [XmlEnum("identifyinq")]
        identifyinq
    }
}