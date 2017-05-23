using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSRequestCardholderPresentCodeEnum
    {
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Store value is 0.
        /// </summary>
        [XmlEnum("0")]
        Normal,

        /// <summary>
        /// Store value is 13.
        /// </summary>
        [XmlEnum("13")]
        Secure3D
    }
}