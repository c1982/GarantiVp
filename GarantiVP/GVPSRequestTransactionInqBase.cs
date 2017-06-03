using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public abstract class GVPSRequestTransactionInqBase
    {
        /// <summary>
        /// Corporate code
        /// <para>Size 3 Byte numeric</para>
        /// <para lang="tr">Kurum kodu</para> 
        /// </summary>
        [XmlElement]
        public ushort InstitutionCode { get; set; }


        /// <summary>
        /// Subscriber code
        /// <para>Size ? Byte alfanumeric</para>
        /// <para lang="tr">Abone / tesisat numarası</para> 
        /// </summary>
        public string SubscriberCode { get; set; }
    }
}