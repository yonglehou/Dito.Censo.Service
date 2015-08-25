using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Route Map Point
    /// </summary>
    [DataContract]
    public class RouteMapPoint : Point
    {
        /// <summary>
        /// Point Order
        /// </summary>
        [DataMember]
        public int Order { get; set; }
    }
}
