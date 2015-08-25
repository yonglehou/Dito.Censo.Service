using System;
using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Peticion para almacenar los datos obtenidos en un pubnto de venta o punto censado
    /// </summary>
    [DataContract]
    public class CensuPointDataRequest
    {
        /// <summary>
        /// Route Id
        /// </summary>
        [DataMember]
        public string RouteId { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [DataMember]
        public string UserId { get; set; }

        /// <summary>
        /// Company Code
        /// </summary>
        [DataMember]
        public string CompanyCode { get; set; }

        /// <summary>
        /// Censu Date Time
        /// </summary>
        [DataMember]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Structure Data Entry
        /// </summary>
        /// <example>
        /// <![CDATA[ 
        /// 
        /// XML Format
        /// <f><e id="1" t="2" c="Dirección" v=""/></f>
        /// 
        /// id = Id Field
        /// f = Form
        /// e = Entry
        /// t = Type { 1=Lista, 2=Texto, 3=Selección Múltiple, 4=Numerico, 5=Imagen }
        /// c = Caption
        /// v = Values
        /// 
        /// ]]>
        /// </example>
        [DataMember]
        public string Data { get; set; }
    }
}
