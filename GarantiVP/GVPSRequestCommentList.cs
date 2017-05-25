using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = false)]
    public class GVPSRequestCommentList
    {
        [XmlElement("Comment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestComment[] Comment { get; set; }
    }
}