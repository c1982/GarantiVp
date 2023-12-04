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
    /// Address information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSAddress
    {
        /// <summary>
        /// Address type. Must be [S] or [B] value.
        /// <para>Size alfanumeric 1 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresstype")]
        public GVPSAddressTypeEnum Type { get; set; }

        /// <summary>
        /// Receivable name
        /// <para>Size 65 Byte</para>
        /// <para lang="tr">Alıcı adı</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        /// <summary>
        /// Receivable last name
        /// <para>Size 32 Byte</para>
        /// <para lang="tr">Alıcı soyadı</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresslastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Company name
        /// <para>Size 40 Byte</para>
        /// <para lang="tr">Firma bilgileri</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresscompany")]
        public string Company { get; set; }

        /// <summary>
        /// Address text
        /// <para>Size 180 Byte</para>
        /// <para lang="tr">Adres metini</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresstext")]
        public string Text { get; set; }

        /// <summary>
        /// District name
        /// <para>Size 25 Byte</para>
        /// <para lang="tr">Semt adı</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddressdistrict")]
        public string District { get; set; }

        /// <summary>
        /// City name
        /// <para>Size 25 Byte</para>
        /// <para lang="tr">Şehir adı</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresscity")]
        public string City { get; set; }

        /// <summary>
        /// Postal code
        /// <para>Size 20 Byte</para>
        /// <para lang="tr">Posta kodu</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresspostalcode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Phone number
        /// <para>Size 30 Byte</para>
        /// <para lang="tr">Telefon numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddressphonenumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Cell phone number
        /// <para>Size 30 Byte</para>
        /// <para lang="tr">Cep telefonu numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddressgsmnumber")]
        public string GSMNumber { get; set; }

        /// <summary>
        /// Fax number
        /// <para>Size 30 Byte</para>
        /// <para lang="tr">Faks numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddressfaxnumber")]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Country
        /// <para>Size ? Byte</para>
        /// <para lang="tr">Ülke</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresscountry")]
        public string Country { get; set; }
    }
}
