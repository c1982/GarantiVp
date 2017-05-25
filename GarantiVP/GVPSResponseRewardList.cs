using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// The field where the prize related transactions are sent.
    /// <para lang="tr">Ödül ile ilgili işlemlerin yollanıldığı alandır.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSResponseRewardList
    {
        /// <summary>
        /// Bonus winnings, Bonus Usage, MR, Company Bonus, Field where the company check transactions are sent.
        /// <para lang="tr">Bonus kazanım, Bonus Kullanım, MR, Firma Bonus, Firma Çek işlemlerinin gönderildiği alandır.</para>
        /// </summary>
        [XmlElement("Reward", Form =System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSResponseReward[] Reward { get; set; }
    }
}