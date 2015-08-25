using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Point GeoLocalization
    /// </summary>
    [DataContract]
    public class Point
    {
        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember]
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember]
        public double Longitude { get; set; }
    }
}
