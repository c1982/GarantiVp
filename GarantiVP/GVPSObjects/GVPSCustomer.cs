using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Customer information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSCustomer
    {
        /// <summary>
        /// Customer IP address
        /// <para>Size alfanumeric 20 Byte</para> 
        /// <para lang="tr">Müşteri IP Adresi</para> 
        /// </summary>
        [XmlElement]
        public string IPAddress { get; set; }

        /// <summary>
        /// Customer e-mail address
        /// <para>Size alfanumeric 64 Byte</para> 
        /// <para lang="tr">Müşteri e-posta adresi</para> 
        /// </summary>
        [XmlElement]
        public string EmailAddress { get; set; }
    }
}