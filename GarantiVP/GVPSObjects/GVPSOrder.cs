using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description LANG=TR
    /// <summary>
    /// Order information
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSOrder
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
        public GVPSItemList ItemList { get; set; }

        /// <summary>
        /// Contain order addresses.
        /// <para>Used for reporting. If you want to see in the virtual pos screens can be filled.</para>
        /// </summary>
        [XmlElement("AddressList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSAddressList AddressList { get; set; }

        /// <summary>
        /// For special field definations.
        /// <para>Used for reporting. If you want to see in the virtual pos screens can be filled.</para>
        /// <para>But the virtual pos must be sent in the defined and defined order in the screens.</para>
        /// </summary>
        [XmlElement("CommentList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSCommentList CommentList { get; set; }

        /// <summary>
        /// Repetitive transaction information
        /// <para lang="tr">Tekrarlanan işlem bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSRecurring Recurring { get; set; }

        /// <summary>
        /// Description
        /// <para lang="tr">Açıklama</para> 
        /// </summary>
        [XmlElement]
        public string Description { get; set; }

        //TODO Create property StartDate
        //TODO Create property EndDate
        //TODO Create property ListPageNum
    }
}