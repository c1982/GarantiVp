using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType =true)]
    public enum GVPSPaymentTypeEnum
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Credit card
        /// <para lang="tr">Kredi kartı</para> 
        /// </summary>
        [XmlEnum("K")]
        CreditCard,

        /// <summary>
        /// Debit kart
        /// <para lang="tr">Banka kartı</para> 
        /// </summary>
        [XmlEnum("D")]
        DebitCard,

        /// <summary>
        /// Checking account
        /// <para lang="tr">Vadesiz hesap</para> 
        /// </summary>
        [XmlEnum("V")]
        Account,
    }
}