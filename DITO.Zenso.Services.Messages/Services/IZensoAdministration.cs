using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DITO.Zenso.Services.Messages
{
    /// <summary>
    /// Servicio de administración de medicamentos
    /// </summary>
    [ServiceContract]
    public interface IZensoAdministration
    {
        /// <summary>
        /// Save Trace Data
        /// </summary>
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveTrace")]
        bool SaveTrace(SaveTraceRequest request);

        /// <summary>
        /// Save Censu Point
        /// </summary>
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveCensu")]
        bool SaveCensuPoint(CensuPointDataRequest request);

        /// <summary>
        /// Retrieve All Censu Data Filter By RouteId
        /// </summary>
        /// <param name="routeid">Route Id</param>
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "CensuData/{routeid}")]
        List<Censu> GetCensuData(string routeid);
    }
}
