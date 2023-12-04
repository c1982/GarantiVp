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

using System.Xml.Serialization;

namespace GarantiVP
{
    /// <summary>
    /// Trading card transaction information
    /// As much as the installment number, it should be installment information.
    /// <para lang="tr">Ortak kart işlemleri bilgisi</para> 
    /// <para lang="tr">Taksit sayısı kadar vade bilgisi olmalıdır.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSCommercialCardExtendedCredit
    {
        /// <summary>
        /// Installment information
        /// <para lang="tr">Taksit bilgileri</para> 
        /// </summary>
        [XmlElement("PaymentList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSTransactionPaymentList PaymentList { get; set; }
    }
}
