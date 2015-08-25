using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Respuesta de inicio de sesión
    /// </summary>
    [DataContract]
    public class LoginResponse
    {
        /// <summary>
        /// Basic Constructor
        /// </summary>
        public LoginResponse()
        {
            Routes = new List<Route>();
        }

        /// <summary>
        /// Usuario
        /// </summary>
        [DataMember]
        public User User { get; set; }
        /// <summary>
        /// User Is Authenticated
        /// </summary>
        [DataMember]
        public bool IsAuthenticated { get; set; }
        /// <summary>
        /// Authenticated Error Message
        /// </summary>
        [DataMember]
        public string AuthenticatedErrorMsg { get; set; }
        /// <summary>
        /// Routes
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<Route> Routes { get; set; }
    }
}
