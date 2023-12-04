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
        [FormElement("orderitemnumber")]
        public uint Number { get; set; }

        /// <summary>
        /// Product / service reference
        /// <para>Size alfanumeric 40 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemproductid")]
        public string ProductID { get; set; }

        /// <summary>
        /// Product / service code
        /// <para>Size alfanumeric 12 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemproductcode")]
        public string ProductCode { get; set; }

        /// <summary>
        /// Purchased quantity information
        /// <para>Size 13 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemquantity")]
        public ulong Quantity { get; set; }

        /// <summary>
        /// Unit amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para>Size numeric 19 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemprice")]
        public ulong Price { get; set; }

        /// <summary>
        /// Total amount
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para>Size numeric 19 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemtotalamount")]
        public ulong TotalAmount { get; set; }

        /// <summary>
        /// Product description
        /// <para>Size 20 Byte</para> 
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemdescription")]
        public string Description { get; set; }
    }
}
