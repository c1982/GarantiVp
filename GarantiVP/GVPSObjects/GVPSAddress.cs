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
        public string LastName { get; set; }

        /// <summary>
        /// Company name
        /// <para>Size 40 Byte</para>
        /// <para lang="tr">Firma bilgileri</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Company { get; set; }

        /// <summary>
        /// Address text
        /// <para>Size 180 Byte</para>
        /// <para lang="tr">Adres metini</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Text { get; set; }

        /// <summary>
        /// District name
        /// <para>Size 25 Byte</para>
        /// <para lang="tr">Semt adı</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string District { get; set; }

        /// <summary>
        /// City name
        /// <para>Size 25 Byte</para>
        /// <para lang="tr">Şehir adı</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string City { get; set; }

        /// <summary>
        /// Postal code
        /// <para>Size 20 Byte</para>
        /// <para lang="tr">Posta kodu</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Phone number
        /// <para>Size 30 Byte</para>
        /// <para lang="tr">Telefon numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Cell phone number
        /// <para>Size 30 Byte</para>
        /// <para lang="tr">Cep telefonu numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GSMNumber { get; set; }

        /// <summary>
        /// Fax number
        /// <para>Size 30 Byte</para>
        /// <para lang="tr">Faks numarası</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Country
        /// <para>Size ? Byte</para>
        /// <para lang="tr">Ülke</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Country { get; set; }
    }
}