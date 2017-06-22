using System.Xml.Serialization;

namespace GarantiVP
{
    //TODO Description
    /// <summary>
    /// 
    /// </summary>
    [XmlType(AnonymousType =true)]
    public class GVPSTransactionResponse
    {
        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Source { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Code { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string ReasonCode { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Message { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string ErrorMsg { get; set; }

        //TODO Description
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string SysErrMsg { get; set; }
    }
}