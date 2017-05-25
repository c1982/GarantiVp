using System.Xml.Serialization;

namespace GarantiVP
{
    [XmlType(AnonymousType =true)]
    public class GVPSResponseTransactionResponse
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
}