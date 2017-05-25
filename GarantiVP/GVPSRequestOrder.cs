using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Order information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRequestOrder
    {
        /// <summary>
        /// Unique order reference.
        /// It is automatically generate by the bank when it is not sent.
        /// <para>Size 36 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrderID { get; set; }

        /// <summary>
        /// Used for reporting.
        /// <para>Size 36 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GroupID { get; set; }

        /// <summary>
        /// Contain order items.
        /// If you want to see in the virtual pos screens can be filled.
        /// </summary>
        [XmlElement("ItemList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestItemList ItemList { get; set; }

        /// <summary>
        /// Contain order addresses.
        /// <para>Used for reporting. If you want to see in the virtual pos screens can be filled.</para>
        /// </summary>
        [XmlElement("AddressList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestAddressList AddressList { get; set; }

        /// <summary>
        /// For special field definations.
        /// <para>Used for reporting. If you want to see in the virtual pos screens can be filled.</para>
        /// <para>But the virtual pos must be sent in the defined and defined order in the screens.</para>
        /// </summary>
        [XmlElement("CommentList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestCommentList CommentList { get; set; }

        //TODO Recurring
        //TODO StartDate
        //TODO EndDate
        //TODO ListPageNum
    }
}