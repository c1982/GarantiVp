using System;
using System.Runtime.Serialization;

namespace GarantiVP
{
    [Serializable]
    public class GVPSExcetion : Exception
    {
        public GVPSExcetion()
        {
        }

        public GVPSExcetion(string message) : base(message)
        {
        }

        public GVPSExcetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GVPSExcetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}