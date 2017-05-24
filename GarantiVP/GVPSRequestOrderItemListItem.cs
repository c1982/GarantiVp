using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Store for order items.
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestOrderItemListItem
    {
        /// <summary>
        /// The order of the product / service
        /// <para>Size 2 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint Number { get; set; }

        /// <summary>
        /// Product / service reference
        /// <para>Size 40 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProductID { get; set; }

        /// <summary>
        /// Product / service code
        /// <para>Size 12 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ProductCode { get; set; }

        /// <summary>
        /// Purchased quantity information
        /// <para>Size 13 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint Quantity { get; set; }

        /// <summary>
        /// Unit amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash</para>
        /// <para>Size 20 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ulong Prince { get; set; }

        /// <summary>
        /// Total amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash</para>
        /// <para>Size 20 Byte</para> 
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