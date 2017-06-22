using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Amount information for recurring payment
    /// <para lang="tr">Ödeme için miktar bilgisi</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSReccurringPayment : GVPSPaymentBase
    {
        #region Just used for Reccurring

        /// <summary>
        /// Payment order number
        /// Beginning with 1, an increment is specified.
        /// <para lang="tr">
        /// </para>Ödeme sıra numarası
        /// 1 den başlayarak, bir artarak belirtilir.</summary>
        [XmlElement]
        public ushort PaymentNum { get; set; }

        #endregion
    }
}
