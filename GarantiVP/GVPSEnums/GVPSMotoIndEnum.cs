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
    /// For ECommerce operations value is N. For Moto operations value is Y.
    /// <para lang="tr">E-ticaret operasyonları için değer N'dir. Moto işlemleri için değer Y'dir.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum GVPSMotoIndEnum
    {
        /// <summary>
        /// Unspecified
        /// <para lang="tr">Belirtilmemiş</para> 
        /// </summary>
        [XmlEnum("")]
        Unspecified,

        /// <summary>
        /// E-Commerce operation
        /// <para>Stored value is N</para>
        /// <para lang="tr">E-ticaret işlemleri</para> 
        /// </summary>
        [XmlEnum("N")]
        ECommerce,

        /// <summary>
        /// Moto operations
        /// <para>Stored value is Y</para>
        /// <para lang="tr">Moto işlemler için</para> 
        /// </summary>
        [XmlEnum("Y")]
        Moto,

        //TODO What is MotoInq.H ?
        /// <summary>
        /// Stored value is H
        /// </summary>
        [XmlEnum("H")]
        H
    }
}
