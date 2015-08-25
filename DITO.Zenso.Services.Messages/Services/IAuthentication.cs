using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Servicio de autenticación
    /// </summary>
    [ServiceContract]
    public interface IAuthentication
    {
        /// <summary>
        /// Valida la conectividad del servicio y la base de datos
        /// </summary>
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool Ping();

        /// <summary>
        /// Realiza el inicio de sesion
        /// </summary>
        /// <param name="request">Peticion de inicio de sesion</param>        
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        LoginResponse Login(LoginRequest request);
    }
}
