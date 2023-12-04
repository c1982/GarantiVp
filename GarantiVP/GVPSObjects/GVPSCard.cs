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
    /// Card information
    /// <para lang="tr">Kart bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSCard
    {
        /// <summary>
        /// Card number
        /// <para>Size numeric Min:15, Max:19 Byte</para>
        /// <para lang="tr">Kart numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("cardnumber")]
        public string Number { get; set; }

        /// <summary>
        /// Card expire date. Must be MMYY format.
        /// <para>Size 4 Byte</para>
        /// <para lang="tr">Kart son kullanım tarihi. AAYY biçiminde olmalı.</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("cardexpiredatemonth", "cardexpiredateyear")]
        public string ExpireDate { get; set; }

        /// <summary>
        /// Card CVV number.
        /// <para>Size Min:3, Max:4 Byte (AMEX)</para>
        /// <para lang="tr">Kart CVV numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("cardcvv2")]
        public string CVV2 { get; set; }

        /// <summary>
        /// Cardholder name
        /// <para lang="tr">Kart sahibi adı</para> 
        /// </summary>
        [XmlElement()]
        [FormElement("cardholder")]
        public string CardHolder { get; set; }
    }
}
