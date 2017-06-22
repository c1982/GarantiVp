using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Currency codes
    /// <para lang="tr">Para birimi kodları</para> 
    [XmlType(AnonymousType = true)]
    public enum GVPSCurrencyCodeEnum : int
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Turkish Lira
        /// <para lang="tr">Türk lirası</para>
        /// </summary>
        [XmlEnum("949")]
        TRL = 949,

        /// <summary>
        /// United State Dollar
        /// <para lang="tr">Amerikan doları</para> 
        /// </summary>
        [XmlEnum("840")]
        USD = 840,

        /// <summary>
        /// Euro
        /// </summary>
        [XmlEnum("978")]
        EURO = 978,

        /// <summary>
        /// English pound
        /// <para lang="tr">İngiliz poundu</para> 
        /// </summary>
        [XmlEnum("826")]
        GBP = 826,

        /// <summary>
        /// Japan yen
        /// <para lang="tr">Japon yen'i</para> 
        /// </summary>
        [XmlEnum("392")]
        JPY = 392
    }
}