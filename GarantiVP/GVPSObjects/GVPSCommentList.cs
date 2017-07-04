using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = false)]
    public class GVPSCommentList
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Comment", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSComment[] Comment { get; set; }
    }
}