using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Customer information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestCustomer
    {
        /// <summary>
        /// Customer IP address
        /// </summary>
        [XmlElement]
        public string IPAddress { get; set; }

        /// <summary>
        /// Customer e-mail address
        /// </summary>
        [XmlElement]
        public string EmailAddress { get; set; }
    }
}