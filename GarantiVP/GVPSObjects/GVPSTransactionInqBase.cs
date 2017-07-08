using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public abstract class GVPSTransactionInqBase
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
        [XmlElement]
        [FormElement("utilitypaysubscode")]
        public string SubscriberCode { get; set; }
    }
}