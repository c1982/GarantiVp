using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSMotoIndEnum
    {
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// stored value is N
        /// </summary>
        [XmlEnum("N")]
        ECommerce,

        /// <summary>
        /// Stored value is Y
        /// </summary>
        [XmlEnum("Y")]
        Moto,

        /// <summary>
        /// Stored value is H
        /// </summary>
        [XmlEnum("H")]
        H
    }
}