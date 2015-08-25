using DITO.Zenso.Services.Messages;
using System.Collections.Generic;

namespace DITO.Zenso.Services.Writes
{
    /// <summary>
    /// Servicio de administración de Zenso
    /// </summary>
    public class ZensoAdministration : IZensoAdministration
    {
        /// <summary>
        /// Save Trace
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveTrace(SaveTraceRequest request)
        {
            return (request.Points != null && request.Points.Count > 0);
        }

        /// <summary>
        /// Save Censu Point
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveCensuPoint(CensuPointDataRequest request)
        {
            return (request != null && !string.IsNullOrEmpty(request.Data));
        }

        /// <summary>
        /// Retrieve All Censu Data Filter By RouteId
        /// </summary>
        /// <param name="routeid"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<Censu> GetCensuData(string routeid)
        {
            List<Censu> response = new List<Censu>();

            Censu newCensu = new Censu();
            newCensu.Id = 1;
            newCensu.RouteId = 1;
            newCensu.DescriptionLine1 = "Censo #1";
            newCensu.DescriptionLine2 = System.DateTime.Now.ToLongDateString();
            newCensu.Data = "<d><e c=\"Zona\" v=\"2 - Aguacatala\" /><e c=\"Barrio\" v=\"Aguacatala\" /><e c=\"Negocio\" v=\"Licores Med\" /></d>";
            newCensu.Latitude = 6.196080d;
            newCensu.Longitude = -75.589875;

            response.Add(newCensu);

            return response;
        }
    }
}
