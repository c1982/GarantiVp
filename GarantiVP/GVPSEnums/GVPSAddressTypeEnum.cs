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
        /// <para lang="tr">Fatura</para> 
        /// </summary>
        [XmlEnum("B")]
        Billing,

        /// <summary>
        /// Shipping
        /// <para lang="tr">Teslim</para> 
        /// </summary>
        [XmlEnum("S")]
        Shipping
    }
}