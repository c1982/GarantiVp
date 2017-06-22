using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Transaction types
    /// <para lang="tr">İşlem tipleri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSTransactionTypeEnum
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Sales
        /// <para lang="tr">Satış</para> 
        /// </summary>
        [XmlEnum("sales")]
        sales,

        /// <summary>
        /// Void
        /// <para lang="tr">İptal</para> 
        /// </summary>
        [XmlEnum("void")]
        @void,

        /// <summary>
        /// Refund
        /// <para lang="tr">İade</para> 
        /// </summary>
        [XmlEnum("refund")]
        refund,

        /// <summary>
        /// Pre Authorization
        /// <para lang="tr">Ön otorizasyon</para> 
        /// </summary>
        [XmlEnum("preauth")]
        preauth,

        /// <summary>
        /// Post Authorization
        /// <para lang="tr">Ön otorizasyon kapatma</para> 
        /// </summary>
        [XmlEnum("postauth")]
        postauth,

        /// <summary>
        /// Identify inquiry
        /// <para lang="tr">Kimlik sorgulama</para> 
        /// </summary>
        [XmlEnum("identifyinq")]
        identifyinq,

        /// <summary>
        /// Order inquiry
        /// <para lang="tr">Sipariş sorgulama</para> 
        /// </summary>
        [XmlEnum("orderinq")]
        orderinq,

        /// <summary>
        /// Order history inquiry
        /// <para lang="tr">Sipariş detay sorgulama</para> 
        /// </summary>
        [XmlEnum("orderhistoryinq")]
        orderhistoryinq,

        /// <summary>
        /// Reward inquiry
        /// <para lang="tr">Bonus / Ödül sorgulama</para> 
        /// </summary>
        [XmlEnum("rewardinq")]
        rewardinq,

        /// <summary>
        /// Settlement inquriy
        /// <para lang="tr">Günsonu / Uzlaşma sorgusu</para> 
        /// </summary>
        [XmlEnum("settlementinq")]
        settlementinq,

        /// <summary>
        /// Extented credit
        /// <para lang="tr">Tüketici / Uzatılmış kredi</para> 
        /// </summary>
        [XmlEnum("extentedcredit")]
        extentedcredit,

        /// <summary>
        /// Extented credit inquriy
        /// <para lang="tr">Tüketici / Uzatılmış kredi sorgulama</para> 
        /// </summary>
        [XmlEnum("extendedcreditinq")]
        extendedcreditinq,

        /// <summary>
        /// Commercial card extended credit
        /// <para lang="tr">Ticari kredi kartı kredisi / Ortak kart işlemi</para> 
        /// </summary>
        [XmlEnum("commercialcardextendedcredit")]
        commercialcardextendedcredit,

        /// <summary>
        /// Utility payment
        /// <para lang="tr">Fatura ödeme</para> 
        /// </summary>
        [XmlEnum("utilitypayment")]
        utilitypayment,

        /// <summary>
        /// Utility payment inquriy
        /// <para lang="tr">Fatura ödeme sorgulama</para> 
        /// </summary>
        [XmlEnum("utilitypaymentInq")]
        utilitypaymentInq,

        /// <summary>
        /// Utility payment void inquiry
        /// <para lang="tr">Fatura iptal sorgulama</para> 
        /// </summary>
        [XmlEnum("utilitypaymentvoidInq")]
        utilitypaymentvoidInq,

        /// <summary>
        /// GSM unit inquiry
        /// <para lang="tr">GSM TRL yükleme sorgulama</para> 
        /// </summary>
        [XmlEnum("gsmunitinq")]
        gsmunitinq,

        /// <summary>
        /// GSM unit sales
        /// <para lang="tr">GSM TRL yükleme</para> 
        /// </summary>
        [XmlEnum("gsmunitsales")]
        gsmunitsales,

        /// <summary>
        /// CepBank operation
        /// <para lang="tr">CepBank işlemi</para> 
        /// </summary>
        [XmlEnum("cepbank")]
        cepbank,

        /// <summary>
        /// CepBank operation inquiry
        /// <para lang="tr">CepBank işlemi sorgulama</para> 
        /// </summary>
        [XmlEnum("cepbankInq")]
        cepbankInq,

        /// <summary>
        /// CepBank reward inquiry
        /// <para lang="tr">CepBank bonus sorgulama</para> 
        /// </summary>
        [XmlEnum("cepbankbonus")]
        cepbankbonus,

        /// <summary>
        /// CepBank cancel / refund
        /// <para lang="tr">CepBank iptal / iade</para> 
        /// </summary>
        [XmlEnum("cepbankvoid")]
        cepbankvoid,

        /// <summary>
        /// Sales with company card
        /// <para lang="tr">Firma kartı ile satışı</para> 
        /// </summary>
        [XmlEnum("firmcardsales")]
        firmcardsales,

        /// <summary>
        /// Canceling pending transactions for recurring payments
        /// <para lang="tr">Tekrarlayan ödemenin bekleyen işlemlerin iptal edilmesi</para> 
        /// </summary>
        [XmlEnum("recurringvoid")]
        recurringvoid,
    }
}