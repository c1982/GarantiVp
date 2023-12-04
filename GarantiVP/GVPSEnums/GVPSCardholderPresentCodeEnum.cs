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
    /// For normal operations value is 0. For 3D secure operations value is 13.
    /// <para>Size 2 Byte numeric</para> 
    /// <para lang="tr">Normal işlemler için değer 0'dır. 3D güvenli işlemler için değer 13'tür.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSCardholderPresentCodeEnum
    {
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// For normal operation
        /// <para>Store value is 0.</para> 
        /// <para lang="tr">Normal işlemler için</para> 
        /// </summary>
        [XmlEnum("0")]
        Normal,

        /// <summary>
        /// For 3D Secure operations
        /// <para>Store value is 13.</para>
        /// <para lang="tr">3D Secure işlemleri için</para> 
        /// </summary>
        [XmlEnum("13")]
        Secure3D
    }
}
