using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSRequestAddressList
    {
        [XmlElement("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestAddress[] Address { get; set; }
    }
}