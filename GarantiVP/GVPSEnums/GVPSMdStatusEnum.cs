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
    /// 3D Secure status codes
    /// <para lang="tr">3D Secure durum kodları</para>
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSMdStatusEnum
    {
        [XmlEnum("")]
        Undefined = -1,

        /// <summary>
        /// Verification failed.
        /// <para lang="tr">Doğrulama başarısız</para> 
        /// </summary>
        [XmlEnum("0")]
        Fail3DSecureVerificationFailed = 0,

        /// <summary>
        /// Full verification
        /// <para lang="tr">Tam doğrulama</para> 
        /// </summary>
        [XmlEnum("1")]
        Full = 1,

        /// <summary>
        /// Card holder or bank not registered in the system.
        /// <para lang="tr">Kart sahibi veya banka sisteme kayıtlı değil.</para> 
        /// </summary>
        [XmlEnum("2")]
        HalfCardHolderOrBankUnknow = 2,

        /// <summary>
        /// Bank not registered in the system. (Buffer)
        /// <para lang="tr">Banka sisteme kayıtlı değil. (Önbellekten)</para> 
        /// </summary>
        [XmlEnum("3")]
        HalfBankUnknow = 3,

        /// <summary>
        /// Verification test. The card owner has opted to register later on the system.
        /// <para lang="tr">Doğrulama denemesi. Kart sahibi sisteme daha sonra kayıt olmayı seçmiş.</para> 
        /// </summary>
        [XmlEnum("4")]
        HalfVerificationTest = 4,

        /// <summary>
        /// Verification failed. (Other bank)
        /// <para lang="tr">Doğrulama başarısız. (Diğer banka)</para> 
        /// </summary>
        [XmlEnum("5")]
        FailVerification = 5,

        /// <summary>
        /// 3D Secure error. May be merchant not have register with 3D Secure
        /// <para lang="tr">3D Secure hatası. İşyeri 3D Secure sistemine kayıt olmamış olabilir. </para> 
        /// </summary>
        [XmlEnum("6")]
        FailSecureError = 6,

        /// <summary>
        /// System error
        /// <para lang="tr">Sistem hatası</para> 
        /// </summary>
        [XmlEnum("7")]
        FailSystemError = 7,

        /// <summary>
        /// Unknow card number.
        /// <para lang="tr">Bilinmeyen kart numarası</para> 
        /// </summary>
        [XmlEnum("8")]
        FailUnknowCardNo = 8,
    }
}
