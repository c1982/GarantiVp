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
    /// Information required for invoice payment transactions
    /// <para lang="tr">Fatura ödeme işlemleri için gerekli bilgiler</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSUtilityPayment : GVPSTransactionInqBase 
    {
        /// <summary>
        /// Invoce ID
        /// <para>Size ? Byte alfanumeric</para> 
        /// <para lang="tr">Fatura numarası</para> 
        /// </summary>
        [XmlElement]
        [FormElement("utilitypayinvoiceid")]
        public string InvoiceID { get; set; }

        /// <summary>
        /// Amount
        /// <para>Size 19 Byte numeric</para> 
        /// <para>WARNING : Do not send more than 2 digits after the decimal slash. Example 1234.567 -> 123456</para>
        /// <para lang="tr">Tutar</para> 
        /// </summary>
        [XmlElement]
        public ulong Amount { get; set; }
    }
}
