using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Censu
    /// </summary>
    [DataContract]
    public class Censu : Point
    {
        /// <summary>
        /// Censu ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Route ID
        /// </summary>
        [DataMember]
        public int RouteId { get; set; }

        /// <summary>
        /// Censu Description
        /// </summary>
        [DataMember]
        public string DescriptionLine1 { get; set; }

        /// <summary>
        /// Censu Description
        /// </summary>
        [DataMember]
        public string DescriptionLine2 { get; set; }

        /// <summary>
        /// Censu Detail Data
        /// <![CDATA[
        /// <d><e c="Zona" v="2 - Aguacatala" /></d>
        /// 
        /// d = Data
        /// e = Elemento
        /// C = Caption
        /// v = Value
        /// 
        /// ]]>
        /// </summary>
        public string Data { get; set; }
    }
}
