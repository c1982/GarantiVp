using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Bonus winnings, Bonus Usage, MR, Company Bonus, Field where the company check transactions are sent.
    /// <para lang="tr">Bonus kazanım, Bonus Kullanım, MR, Firma Bonus, Firma Çek işlemlerinin gönderildiği alandır.</para>
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSReward
    {
        /// <summary>
        /// Prize type
        /// <para lang="tr">Ödül tipi</para>
        /// <para>Alfanumeric</para> 
        /// </summary>
        [XmlElement("Type")]
        public GVPSRewardTypeEnum Type { get; set; }

        /// <summary>
        /// Amount of reward used
        /// <para lang="tr">Kullanılan ödül tutarı</para>
        /// <para>Numeric</para>
        /// <para>WARNING : The two digits to the right are decimal number fields. Divide by 100.</para>
        /// <para lang="tr">UYARI : Sağdaki iki hane ondalıklı sayı alanıdır. 100'e bölünüz.</para>
        /// </summary>
        [XmlElement("TotalAmount")]
        public ulong TotalAmount { get; set; }

        /// <summary>
        /// Amount of prizes earned
        /// <para lang="tr">Kazanılan ödül tutarı</para> 
        /// <para>Numeric</para>
        /// <para>WARNING : The two digits to the right are decimal number fields. Divide by 100.</para>
        /// <para lang="tr">UYARI : Sağdaki iki hane ondalıklı sayı alanıdır. 100'e bölünüz.</para>
        /// </summary>
        [XmlElement("LastTxnGainAmount")]
        public ulong LastTxnGainAmount { get; set; }

    }
}