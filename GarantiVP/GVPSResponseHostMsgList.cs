using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType = true)]
    public class GVPSResponseHostMsgList
    {
        [XmlElement("HostMsg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] HostMsg { get; set; }
    }
}