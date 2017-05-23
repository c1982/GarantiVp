using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSAddressTypeEnum
    {
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Billing
        /// </summary>
        [XmlEnum("B")]
        Billing,

        /// <summary>
        /// Shipping
        /// </summary>
        [XmlEnum("S")]
        Shipping
    }
}