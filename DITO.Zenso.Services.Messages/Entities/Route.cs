using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Route
    /// </summary>
    [DataContract]
    public class Route
    {
        /// <summary>
        /// basic Constructor
        /// </summary>
        public Route()
        {
            Map = new List<RouteMapPoint>();
            TrackingDetail = new List<TracePoint>();
        }

        /// <summary>
        /// Route ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Route Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Route Description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Route Start DateTime
        /// </summary>
        [DataMember]
        public DateTime Start { get; set; }

        /// <summary>
        /// Route End DateTime
        /// </summary>
        [DataMember]
        public DateTime End { get; set; }

        /// <summary>
        /// Route State => 
        /// 0 = Finalizada | 1 = Pendiente (Por Ejecutar) | 2 = Inicializada
        /// </summary>
        [DataMember]
        public int State { get; set; }

        /// <summary>
        /// Route Map
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<RouteMapPoint> Map { get; set; }

        /// <summary>
        /// Route TRacking Points Detail
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<TracePoint> TrackingDetail { get; set; }

        /// <summary>
        /// Structure Data Entry
        /// Contains the form layout to enter data for each point
        /// 
        /// XML Format
        /// <f><e id="1" t="2" c="Dirección" v=""/></f>
        /// 
        /// id = Id Field
        /// f = Form
        /// e = Entry
        /// t = Type { 1=Lista, 2=Texto, 3=Selección Múltiple, 4=Numerico, 5=Imagen, 6=Texto Largo }
        /// c = Caption
        /// v = Values
        /// </summary>
        [DataMember]
        public string StrucDataEntry { get; set; }
    }
}
