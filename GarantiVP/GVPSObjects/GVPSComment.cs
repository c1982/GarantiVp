using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Order comment
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSComment
    {

        /// <summary>
        /// Comment no
        /// <para>But the virtual pos must be sent in the defined and defined order in the screens.</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("ordercommentnumber")]
        public uint Number { get; set; }


        /// <summary>
        /// Comment
        /// <para>Size 20 Byte</para>
        /// </summary>
        [XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("ordercommenttext")]
        public string Text { get; set; }
    }
}