using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public enum GVPSRecurringTypeEnum
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Repetitive
        /// <para lang="tr">Tekrarlanan</para> 
        /// </summary>
        [XmlEnum("R")]
        Repetitive
    }
}