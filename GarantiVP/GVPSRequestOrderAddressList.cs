using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSRequestOrderAddressList
    {
        [XmlElement("Address", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestOrderAddressListAddress[] Address { get; set; }
    }
}