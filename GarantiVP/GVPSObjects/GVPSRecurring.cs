using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Repetitive transaction information
    /// <para lang="tr">Tekrarlanan işlem bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRecurring
    {

        /// <summary>
        /// Repeat type
        /// <para>Size alfanumeric 1 Byte</para> 
        /// <para lang="tr">Tekrarlama tipi</para>
        /// </summary>
        [XmlElement]
        public GVPSRecurringTypeEnum Type { get; set; }

        /// <summary>
        /// Total payment count
        /// <para>Size numeric 3 Byte</para> 
        /// <para lang="tr">Toplam ödeme sayısı</para> 
        /// </summary>
        [XmlElement]
        public ushort TotalPaymentNum { get; set; }

        /// <summary>
        /// Repeat frequency
        /// <para lang="tr">Tekrar sıklığı</para> 
        /// </summary>
        public GVPSFrequencyTypeEnum FrequencyType { get; set; }

        /// <summary>
        /// Recurrence frequency
        /// <para>Size 3 Byte</para> 
        /// <para>When 2 is given for <paramref name="FrequencyType"/> = <see cref="GVPSFrequencyTypeEnum.Weekly"/>, the pay is taken every 2 weeks.</para>
        /// <para lang="tr">Tekrarlama sıklığı sayısı</para>
        /// <para lang="tr"><paramref name="FrequencyType"/> = <see cref="GVPSFrequencyTypeEnum.Weekly"/> için, 2 değeri verildiğinde ödeme her 2 haftada bir alınır.</para> 
        /// </summary>
        public ushort FrequencyInterval { get; set; }

        /// <summary>
        /// First payment date.
        /// <para>Size 8 Byte, Format must be YYYYMMDD</para>
        /// <para lang="tr">İlk ödeme tarihi</para>
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Amount information for variable recurrent payment
        /// <para lang="tr">Değişken tekrarlayan ödeme için tutar bilgileri</para> 
        /// </summary>
        [XmlElement("PaymentList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSReccuringPaymentList PaymentList { get; set; }
    }
}