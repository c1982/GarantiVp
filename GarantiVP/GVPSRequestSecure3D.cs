using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Information required for 3D secure operations.
    /// <para>The PayerAuthcode field should be 13 for 3D operations.</para>
    /// <para lang="tr">3D secure işlemleri için gerekli bilgiler.</para> 
    /// <para lang="tr">PayerAuthcode alanı 3D işlemler için 13 değerinde olmalıdır.</para> 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSRequestSecure3D
    {
        /// <summary>
        /// Cardholder Authentication Verification Value
        /// <para>Size 64 Byte alfanumeric</para>
        /// <para lang="tr">Kart Sahibi Doğrulama Doğrulama Değeri</para>
        /// </summary>
        [XmlElement]
        public string AuthenticationCode { get; set; }

        /// <summary>
        /// Electronic Commerce Indicator value (ECI)
        /// <para>Size 2 Byte numeric</para>
        /// <para lang="tr">Elektronik Ticaret Gösterge numarası</para> 
        /// </summary>
        [XmlElement]
        public ushort SecurityLevel { get; set; }

        /// <summary>
        /// Transaction ID 
        /// A value that uniquely identifies the transaction
        /// <para>Size 50 Byte alfanumeric</para>
        /// <para lang="tr">İşlem kimlik bilgisi</para> 
        /// </summary>
        [XmlElement]
        public string TxnID { get; set; }

        //TODO create Md status enum
        /// <summary>
        /// Status information for processing
        /// <para>Size ? Byte alfanumeric</para>
        /// <para lang="tr">İşleme ait durum bilgisi</para> 
        /// </summary>
        [XmlElement]
        public string Md { get; set; }
    }
}