using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// For normal operations value is 0. For 3D secure operations value is 13.
    /// <para>Size 2 Byte numeric</para> 
    /// <para lang="tr">Normal işlemler için değer 0'dır. 3D güvenli işlemler için değer 13'tür.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSCardholderPresentCodeEnum
    {
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// For normal operation
        /// <para>Store value is 0.</para> 
        /// <para lang="tr">Normal işlemler için</para> 
        /// </summary>
        [XmlEnum("0")]
        Normal,

        /// <summary>
        /// For 3D Secure operations
        /// <para>Store value is 13.</para>
        /// <para lang="tr">3D Secure işlemleri için</para> 
        /// </summary>
        [XmlEnum("13")]
        Secure3D
    }
}