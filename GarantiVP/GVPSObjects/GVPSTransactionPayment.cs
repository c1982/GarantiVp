/**
MIT License

Copyright (c) 2014 Oğuzhan YILMAZ and contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
**/

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
