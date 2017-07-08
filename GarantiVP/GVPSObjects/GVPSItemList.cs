using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Contain for order items.
    /// <para lang="tr">Sipariş kalemlerini taşır</para> 
    /// </summary>
    [XmlType(AnonymousType=true)]
    public class GVPSItemList
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [FormElement("orderitemcount")]
        public GVPSItem[] Item { get; set; }
    }
}