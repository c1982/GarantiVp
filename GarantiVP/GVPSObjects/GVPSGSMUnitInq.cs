using System.Xml.Serialization;

namespace GarantiVP
{

    /// <summary>
    /// GSM money uploading, query processing parameters
    /// <para lang="tr">GSM para yükleme, sorgulama işlemi parametreleri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSGSMUnitInq : GVPSTransactionInqBase
    {
        /// <summary>
        /// It contains information such as package ID, amount, and so on.
        /// <para>Size ? Byte alfanumeric</para> 
        /// <para lang="tr">Paket ID, tutar ve benzeri gibi bilgileri içerir.</para> 
        /// </summary>
        [XmlElement]
        [FormElement("gsmquantity")]
        public string Quantity { get; set; }
    }
}