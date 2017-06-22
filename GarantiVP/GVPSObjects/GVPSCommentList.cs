using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = false)]
    public class GVPSCommentList
    {
        [XmlElement("Comment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSComment[] Comment { get; set; }
    }
}