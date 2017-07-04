using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Terminal information 
    /// <para lang="tr">Terminal bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSTerminal
    {
        /// <summary>
        /// Provisional user code for the terminal.
        /// <para>Size 32 Byte</para>
        /// <value>
        /// <para>PROVAUT is used as a provisioning user.</para>
        /// <para>PROVRFN is the user used for cancellation and return processes.</para>
        /// <para lang="tr">Provizyon kullanıcı kodu</para> 
        /// </value>
        /// </summary>
        [XmlElement]
        public string ProvUserID { get; set; }

        /// <summary>
        /// User - request validation hash
        /// <para lang="tr">Kullanıcı - istek doğrulama özeti</para> 
        /// </summary>
        [XmlElement]
        public string HashData { get; set; }

        /// <summary>
        /// The user performing the transaction is the name.
        /// <para>Size 32 Byte</para>
        /// <para>In the Call Center and similar channels, the code of the salesperson performing the sales transaction is written in this field.</para>
        /// <para lang="tr">İşlemi gerçekleştiren kullanıcı adıdır.</para>
        /// <para lang="tr">Çağrı Merkezi ve benzeri kanallarda, satış işlemini gerçekleştiren satış elemanının kodu bu alanda yazılmıştır.</para> 
        /// </summary>
        [XmlElement]
        public string UserID { get; set; }

        /// <summary>
        /// Terminal ID
        /// <para>Size Numeric 9 Byte</para>
        /// <para lang="tr">Terminal referansı</para> 
        /// </summary>
        [XmlElement]
        public string ID { get; set; }

        /// <summary>
        /// Merchant reference
        /// <para>Size numeric 9 Byte</para>
        /// <para lang="tr">Satıcı referansı</para>
        /// </summary>
        [XmlElement]
        public string MerchantID { get; set; }

        /// <summary>
        /// Sub merchant reference
        /// <para lang="tr">Alt bayii referans kodu</para> 
        /// </summary>
        public string SubMerchantID { get; set; }

    }
}