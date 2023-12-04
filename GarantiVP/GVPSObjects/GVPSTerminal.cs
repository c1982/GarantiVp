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
    /// Terminal information 
    /// <para lang="tr">Terminal bilgileri</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSTerminal
    {
        /// <summary>
        /// Provisional user code for the terminal.
        /// <para>Size 32 Byte</para>
        /// <value>
        /// <para>PROVAUT is used as a provisioning user.</para>
        /// <para>PROVRFN is the user used for cancellation and return processes.</para>
        /// <para lang="tr">Provizyon kullanıcı kodu</para> 
        /// </value>
        /// </summary>
        [XmlElement]
        [FormElement("terminalprovuserid")]
        public string ProvUserID { get; set; }

        /// <summary>
        /// User - request validation hash
        /// <para lang="tr">Kullanıcı - istek doğrulama özeti</para> 
        /// </summary>
        [XmlElement]
        public string HashData { get; set; }

        /// <summary>
        /// The user performing the transaction is the name.
        /// <para>Size 32 Byte</para>
        /// <para>In the Call Center and similar channels, the code of the salesperson performing the sales transaction is written in this field.</para>
        /// <para lang="tr">İşlemi gerçekleştiren kullanıcı adıdır.</para>
        /// <para lang="tr">Çağrı Merkezi ve benzeri kanallarda, satış işlemini gerçekleştiren satış elemanının kodu bu alanda yazılmıştır.</para> 
        /// </summary>
        [XmlElement]
        [FormElement("terminaluserid")]
        public string UserID { get; set; }

        /// <summary>
        /// Terminal ID
        /// <para>Size Numeric 9 Byte</para>
        /// <para lang="tr">Terminal referansı</para> 
        /// </summary>
        [XmlElement]
        [FormElement("terminalid")]
        public string ID { get; set; }

        /// <summary>
        /// Merchant reference
        /// <para>Size numeric 9 Byte</para>
        /// <para lang="tr">Satıcı referansı</para>
        /// </summary>
        [XmlElement]
        [FormElement("terminalmerchantid")]
        public string MerchantID { get; set; }

        /// <summary>
        /// Sub merchant reference
        /// <para lang="tr">Alt bayii referans kodu</para> 
        /// </summary>
        public string SubMerchantID { get; set; }

    }
}
