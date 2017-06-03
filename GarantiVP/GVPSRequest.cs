namespace GarantiVP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSRequest", Namespace = null)]
    public class GVPSRequest
    {
        /// <summary>
        /// Operation mode
        /// <para lang="tr">İşlem modu</para> 
        /// </summary>
        [XmlElement]
        public GVPSRequestModeEnum Mode { get; set; }

        /// <summary>
        /// API version used
        /// <para>Size 16 Byte alfanumeric</para> 
        /// <para lang="tr">Kullanılan api sürümü</para> 
        /// </summary>
        [XmlElement]
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
        public GVPSRequestTerminal Terminal { get; set; }

        /// <summary>
        /// Customer information
        /// <para lang="tr">Müşteri bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSRequestCustomer Customer { get; set; }

        /// <summary>
        /// Card information
        /// <para lang="tr">Kart bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSRequestCard Card { get; set; }

        /// <summary>
        /// Order information
        /// <para lang="tr">Sipariş bilgileri</para> 
        /// </summary>
        [XmlElement]
        public GVPSRequestOrder Order { get; set; }

        /// <summary>
        /// Transaction and financial information
        /// <para lang="tr">İşlem ve finansal bilgiler</para> 
        /// </summary>
        [XmlElement]
        public GVPSRequestTransaction Transaction { get; set; }

        /// <summary>
        /// For end-of-day inquiries
        /// <para lang="tr">Gün sonu sorgulamaları için</para> 
        /// </summary>
        [XmlElement]
        public GVPSRequestSettlementInq SettlementInq { get; set; }

    }

}
