using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Amount information for transaction payment
    /// <para lang="tr">Ödeme için miktar bilgisi</para> 
    /// </summary>
    public class GVPSTransactionPayment : GVPSPaymentBase
    {
        #region Just used for Transaction

        /// <summary>
        /// Payment order number
        /// Beginning with 1, an increment is specified.
        /// <para>Size 2 Byte numeric</para> 
        /// <para lang="tr">
        /// </para>Ödeme sıra numarası
        /// 1 den başlayarak, bir artarak belirtilir.</summary>
        [XmlElement]
        public ushort Number { get; set; }

        /// <summary>
        /// Installment date
        /// <para>Size 8 Byte numeric, Format yyyyMMdd</para> 
        /// <para lang="tr">
        /// </para>Taksit tarihi</summary>
        [XmlElement]
        public string DueDate { get; set; }

        #endregion

    }
}
