using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Contain for order items.
    /// </summary>
    [XmlType(AnonymousType=true)]
    public class GVPSRequestOrderItemList
    {
        [XmlElement("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSRequestOrderItemListItem[] Item { get; set; }
    }
}