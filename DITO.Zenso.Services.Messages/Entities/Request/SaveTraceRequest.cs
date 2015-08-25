using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Peticion de inicio de sesion
    /// </summary>
    [DataContract]
    public class SaveTraceRequest
    {
        /// <summary>
        /// Basic Constructor
        /// </summary>
        public SaveTraceRequest()
        {
            Points = new List<TracePoint>();
        }

        /// <summary>
        /// Route ID
        /// </summary>
        [DataMember]
        public int RouteId { get; set; }

        /// <summary>
        /// Points Traced
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<TracePoint> Points { get; set; }
    }
}
