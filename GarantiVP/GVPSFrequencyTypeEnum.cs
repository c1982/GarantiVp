using System.Xml.Serialization;

namespace GarantiVP
{

    [XmlType(AnonymousType = true)]
    public enum GVPSFrequencyTypeEnum
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Daily
        /// <para lang="tr">Günlük</para> 
        /// </summary>
        [XmlEnum("D")]
        Daily,

        /// <summary>
        /// Weekly
        /// <para lang="tr">Haftalık</para> 
        /// </summary>
        [XmlEnum("W")]
        Weekly,

        /// <summary>
        /// Mountly
        /// <para lang="tr">Aylık</para> 
        /// </summary>
        [XmlEnum("M")]
        Mounthly,

        /// <summary>
        /// Yearly
        /// <para lang="tr">Yıllık</para> 
        /// </summary>
        [XmlEnum("Y")]
        Yearly
    }
}