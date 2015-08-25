using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Usuario
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        [DataMember]
        public string UserId { get; set; }
        /// <summary>
        /// Full Name
        /// </summary>
        [DataMember]
        public string FullName { get; set; }
        /// <summary>
        /// Photo
        /// </summary>
        [DataMember]
        public string PhotoURL { get; set; }
        /// <summary>
        /// Active Route
        /// </summary>
        public Route ActiveRoute { get; set; }
    }
}
