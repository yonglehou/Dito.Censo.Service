using System.Runtime.Serialization;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Peticion de inicio de sesion
    /// </summary>
    [DataContract]
    public class LoginRequest
    {
        /// <summary>
        /// Usuario
        /// </summary>
        [DataMember]
        public string User { get; set; }

        /// <summary>
        /// Contraseña
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Company Code
        /// </summary>
        [DataMember]
        public string CiaId { get; set; }
    }
}
