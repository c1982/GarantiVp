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

namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class GVPSPacked
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore()]
        public string RawRequest { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore()]
        public string RawResponse { get; set; }


        /// <summary>
        /// Operation mode
        /// <para lang="tr">İşlem modu</para> 
        /// </summary>
        [XmlElement]
        [FormElement("mode")]
        public GVPSRequestModeEnum Mode { get; set; }

        /// <summary>
        /// API version used
        /// <para>Size 16 Byte alfanumeric</para> 
        /// <para lang="tr">Kullanılan api sürümü</para> 
        /// </summary>
        [XmlElement]
        [FormElement("apiversion")]
        public string Version { get; set; }

        /// <summary>
        /// Channel code
        /// <para>ChannelCode field must be one of 'A,R,S,O,P,D,T' chars.</para>
        /// <para>Size 1 Byte alfanumeric</para> 
        /// <para lang="tr">Kanal kodu.</para> 
        /// </summary>
        [XmlElement]
        public string ChannelCode { get; set; }

        /// <summary>
        /// Virtual pos validation parameters
        /// <para lang="tr">Sanal pos doğrulama parametreleri</para> 
        /// </summary>
        [XmlElement]
        public GVPSTerminal Terminal { get; set; }

        /// <summary>
        /// Customer information
        /// <para lang="tr">Müşteri bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSCustomer Customer { get; set; }

        /// <summary>
        /// Card information
        /// <para lang="tr">Kart bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSCard Card { get; set; }

        /// <summary>
        /// Order information
        /// <para lang="tr">Sipariş bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSOrder Order { get; set; }

        /// <summary>
        /// Transaction and financial information
        /// <para lang="tr">İşlem ve finansal bilgiler</para> 
        /// </summary>
        [XmlElement]
        public GVPSTransaction Transaction { get; set; }

        /// <summary>
        /// For end-of-day inquiries
        /// <para lang="tr">Gün sonu sorgulamaları için</para> 
        /// </summary>
        [XmlElement]
        public GVPSSettlementInq SettlementInq { get; set; }

    }

}
