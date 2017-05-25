using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Contain for order items.
    /// </summary>
    [XmlType(AnonymousType=true)]
    public class GVPSRequestItemList
    {
        [XmlElement("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestItem[] Item { get; set; }
    }
}