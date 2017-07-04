using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSAddressList
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderaddresscount")]
        public GVPSAddress[] Address { get; set; }
    }
}