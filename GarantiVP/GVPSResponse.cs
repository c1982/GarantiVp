namespace GarantiVP
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("GVPSResponse", Namespace = null)]
    public class GVPSResponse
    {
        [XmlElement("Order")]
        public ResponseOrder Order { get; set; }

        [XmlElement(ElementName = "Transaction")]
        public ResponseTransaction Transaction { get; set; }

        public string RawRequest { get; set; }

        public string RawResponse { get; set; }
    }

    public class ResponseOrder
    {
        [XmlElement]
        public string OrderID { get; set; }
    }

    public class ResponseTransaction
    {
        [XmlElement]
        public Response Response { get; set; }

        [XmlElement]
        public string RetrefNum { get; set; }

        [XmlElement]
        public string AuthCode { get; set; }

        [XmlElement]
        public string BatchNum { get; set; }

        [XmlElement]
        public string SequenceNum { get; set; }

        [XmlElement]
        public string ProvDate { get; set; }

        [XmlElement]
        public string HostMsgList { get; set; }

        [XmlElement]
        public RewardInqResult RewardInqResult { get; set; }
    }

    public class Response
    {

        [XmlElement]
        public string Source { get; set; }

        [XmlElement]
        public string Code { get; set; }

        [XmlElement]
        public string ReasonCode { get; set; }

        [XmlElement]
        public string Message { get; set; }

        [XmlElement]
        public string ErrorMsg { get; set; }

        [XmlElement]
        public string SysErrMsg { get; set; }
    }

    public class RewardInqResult
    {
        [XmlElement]
        public string RewardList { get; set; }

        [XmlElement]
        public string ChequeList { get; set; }
    }    
}
