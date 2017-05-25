using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Address information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestAddress
    {
        /// <summary>
        /// Address type
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSAddressTypeEnum Type { get; set; }

        /// <summary>
        /// Receivable name
        /// <para>Size 65 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        /// <summary>
        /// Receivable last name
        /// <para>Size 32 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LastName { get; set; }

        /// <summary>
        /// Company name
        /// <para>Size 40 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Company { get; set; }

        /// <summary>
        /// Address text
        /// <para>Size 180 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Text { get; set; }

        /// <summary>
        /// District name
        /// <para>Size 25 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string District { get; set; }

        /// <summary>
        /// City name
        /// <para>Size 25 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string City { get; set; }

        /// <summary>
        /// Postal code
        /// <para>Size 20 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Phone number
        /// <para>Size 30 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PhoneNumber { get; set; }
    }
}