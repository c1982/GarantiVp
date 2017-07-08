using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// GSM money uploading, query processing parameters
    /// <para lang="tr">GSM para yükleme, sorgulama işlemi parametreleri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSGSMUnitSales : GVPSGSMUnitInq 
    {
        /// <summary>
        /// Deposit no
        /// <para lang="tr">Kontör numarası</para> 
        /// </summary>
        [XmlElement]
        [FormElement("gsmsalesunitid")]
        public string UnitID { get; set; }

        /// <summary>
        /// Amount
        /// <para>Size 19 Byte numeric</para> 
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para lang="tr">Tutar</para> 
        /// </summary>
        [XmlElement]
        [FormElement("gsmsalesamnt")]
        public ulong Amount { get; set; }
    }
}