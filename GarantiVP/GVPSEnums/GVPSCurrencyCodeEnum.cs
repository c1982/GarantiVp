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
    /// Currency codes
    /// <para lang="tr">Para birimi kodları</para> 
    [XmlType(AnonymousType = true)]
    public enum GVPSCurrencyCodeEnum : int
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// Turkish Lira
        /// <para lang="tr">Türk lirası</para>
        /// </summary>
        [XmlEnum("949")]
        TRL = 949,

        /// <summary>
        /// United State Dollar
        /// <para lang="tr">Amerikan doları</para> 
        /// </summary>
        [XmlEnum("840")]
        USD = 840,

        /// <summary>
        /// Euro
        /// </summary>
        [XmlEnum("978")]
        EURO = 978,

        /// <summary>
        /// English pound
        /// <para lang="tr">İngiliz poundu</para> 
        /// </summary>
        [XmlEnum("826")]
        GBP = 826,

        /// <summary>
        /// Japan yen
        /// <para lang="tr">Japon yen'i</para> 
        /// </summary>
        [XmlEnum("392")]
        JPY = 392
    }
}
