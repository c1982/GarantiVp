using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// For CepBank reward inquiry
    /// <para lang="tr">CepBank bonus / ödül sorgulama için</para> 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSRequestCepBankIng
    {
        /// <summary>
        /// Cell phone number
        /// <para lang="tr">Cep telefonu numarası</para> 
        /// </summary>
        [XmlElement]
        public string GSMNumber { get; set; }
    }
}