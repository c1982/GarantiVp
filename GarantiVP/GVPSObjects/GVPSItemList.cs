using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Contain for order items.
    /// </summary>
    [XmlType(AnonymousType=true)]
    public class GVPSItemList
    {
        [XmlElement("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSItem[] Item { get; set; }
    }
}