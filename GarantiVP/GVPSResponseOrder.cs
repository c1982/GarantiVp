using System.Xml.Serialization;

namespace GarantiVP
{

    [XmlType(AnonymousType = true)]
    public class GVPSResponseOrder
    {
        [XmlElement]
        public string OrderID { get; set; }
    }
}