/**
MIT License

Copyright (c) 2014 Oğuzhan YILMAZ and contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
**/

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
