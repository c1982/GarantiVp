using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Prize type
    /// <para lang="tr">Ödül tipi</para>
    /// <para>Alfanumeric</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSRewardTypeEnum
    {
        [XmlEnum("")]
        Unspecified,

        [XmlEnum("FBB")]
        FBB
    }
}