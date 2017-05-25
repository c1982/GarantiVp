using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Terminal information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestTerminal
    {
        /// <summary>
        /// Provisional user code for the terminal.
        /// <para>Size 32 Byte</para>
        /// <para>PROVAUT is used as a provisioning user.</para>
        /// <para>PROVRFN is the user used for cancellation and return processes.</para>
        /// </summary>
        [XmlElement]
        public string ProvUserID { get; set; }

        [XmlElement]
        public string HashData { get; set; }

        /// <summary>
        /// The user performing the transaction is the user.
        /// <para>Size 32 Byte</para>
        /// <para>In the Call Center and similar channels, the code of the salesperson performing the sales transaction is written in this field.</para>
        /// </summary>
        [XmlElement]
        public string UserID { get; set; }

        /// <summary>
        /// Terminal ID
        /// <para>Size 9 Byte </para>
        /// </summary>
        [XmlElement]
        public string ID { get; set; }

        /// <summary>
        /// Merchant reference
        /// </summary>
        [XmlElement]
        public string MerchantID { get; set; }

        /// <summary>
        /// Sub merchant reference
        /// </summary>
        public string SubMerchantID { get; set; }

    }
}