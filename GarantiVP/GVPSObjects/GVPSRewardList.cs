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
    /// The field where the prize related transactions are sent.
    /// <para lang="tr">Ödül ile ilgili işlemlerin yollanıldığı alandır.</para> 
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class GVPSRewardList
    {
        /// <summary>
        /// Bonus winnings, Bonus Usage, MR, Company Bonus, Field where the company check transactions are sent.
        /// <para lang="tr">Bonus kazanım, Bonus Kullanım, MR, Firma Bonus, Firma Çek işlemlerinin gönderildiği alandır.</para>
        /// </summary>
        [XmlElement("Reward", Form =System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public GVPSReward[] Reward { get; set; }
    }
}
