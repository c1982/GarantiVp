using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description LANG=TR
    /// <summary>
    /// Store for order items.
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSItem
    {
        /// <summary>
        /// The order of the product / service. Must be start 1
        /// <para>Size numeric 2 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint Number { get; set; }

        /// <summary>
        /// Product / service reference
        /// <para>Size alfanumeric 40 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProductID { get; set; }

        /// <summary>
        /// Product / service code
        /// <para>Size alfanumeric 12 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProductCode { get; set; }

        /// <summary>
        /// Purchased quantity information
        /// <para>Size 13 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ulong Quantity { get; set; }

        /// <summary>
        /// Unit amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para>Size numeric 19 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ulong Price { get; set; }

        /// <summary>
        /// Total amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para>Size numeric 19 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ulong TotalAmount { get; set; }

        /// <summary>
        /// Product description
        /// <para>Size 20 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Description { get; set; }
    }
}