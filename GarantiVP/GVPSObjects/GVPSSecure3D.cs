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
    /// Information required for 3D secure operations.
    /// <para>The PayerAuthcode field should be 13 for 3D operations.</para>
    /// <para lang="tr">3D secure işlemleri için gerekli bilgiler.</para> 
    /// <para lang="tr">PayerAuthcode alanı 3D işlemler için 13 değerinde olmalıdır.</para> 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSSecure3D
    {
        /// <summary>
        /// Cardholder Authentication Verification Value
        /// <para>Size 64 Byte alfanumeric</para>
        /// <para lang="tr">Kart Sahibi Doğrulama Doğrulama Değeri</para>
        /// </summary>
        [XmlElement]
        public string AuthenticationCode { get; set; }

        /// <summary>
        /// Electronic Commerce Indicator value (ECI)
        /// <para>Size 2 Byte numeric</para>
        /// <para lang="tr">Elektronik Ticaret Gösterge numarası</para> 
        /// </summary>
        [XmlElement]
        public ushort SecurityLevel { get; set; }

        /// <summary>
        /// Transaction ID 
        /// A value that uniquely identifies the transaction
        /// <para>Size 50 Byte alfanumeric</para>
        /// <para lang="tr">İşlem kimlik bilgisi</para> 
        /// </summary>
        [XmlElement]
        public string TxnID { get; set; }

        /// <summary>
        /// Status information for processing
        /// <para>Size ? Byte alfanumeric</para>
        /// <para lang="tr">İşleme ait durum bilgisi</para> 
        /// </summary>
        [XmlElement]
        public GVPSMdStatusEnum Md { get; set; }
    }
}
