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
        [FormElement("orderaddresscount")]
        public GVPSComment[] Comment { get; set; }
    }
}