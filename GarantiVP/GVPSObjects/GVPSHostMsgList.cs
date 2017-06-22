using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSHostMsgList
    {
        [XmlElement("HostMsg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] HostMsg { get; set; }
    }
}