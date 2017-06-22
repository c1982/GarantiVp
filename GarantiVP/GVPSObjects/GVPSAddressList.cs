using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSAddressList
    {
        [XmlElement("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSAddress[] Address { get; set; }
    }
}