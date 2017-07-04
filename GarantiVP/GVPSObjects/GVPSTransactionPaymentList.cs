using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GarantiVP
{

    /// <summary>
    /// Amount information for variable recurrent payment
    /// <para lang="tr">Değişken tekrarlayan ödeme için tutar bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSTransactionPaymentList : GVPSPaymentListBase<GVPSTransactionPayment>
    {
    }
}
