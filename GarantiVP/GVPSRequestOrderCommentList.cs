using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = false)]
    public class GVPSRequestOrderCommentList
    {
        [XmlElement("Comment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestOrderCommentListComment[] Comment { get; set; }
    }
}