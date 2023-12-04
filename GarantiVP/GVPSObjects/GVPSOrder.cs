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
        [FormElement("orderid")]
        public string OrderID { get; set; }

        /// <summary>
        /// Used for reporting.
        /// <para>Size 36 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("ordergroupid")]
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
        [FormElement("orderdescription")]
        public string Description { get; set; }

        //TODO Create property StartDate
        //TODO Create property EndDate
        //TODO Create property ListPageNum
    }
}
