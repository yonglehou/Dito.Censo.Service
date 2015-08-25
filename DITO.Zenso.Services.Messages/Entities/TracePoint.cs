using System;
using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Save Trace Detail
    /// </summary>
    [DataContract]
    public class TracePoint : Point
    {   
        /// <summary>
        /// DateTime
        /// </summary>
        [DataMember]
        public DateTime DateTime { get; set; }
    }
}
