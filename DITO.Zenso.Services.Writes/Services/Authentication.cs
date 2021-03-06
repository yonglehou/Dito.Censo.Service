﻿using DITO.Zenso.Services.Messages;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DITO.Zenso.Services.Writes
{
    /// <summary>
    /// Servicio de autenticación
    /// </summary>
    public class Authentication : IAuthentication
    {
        /// <summary>
        /// Valida la conectividad del servicio y la base de datos
        /// </summary>
        public bool Ping()
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(DBProvider.GetConectionString(null)))
                {
                    SqlCommand Cmd = new SqlCommand("SELECT Id, Nom, Ape FROM Empleados ORDER BY Nom", Conn);
                    Conn.Open();

                    SqlDataReader reader = Cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        IDataRecord Row = reader as IDataRecord;

                        if (Row != null)
                        {
                            var Id = Row[0];
                            var Nom = Row[1];
                            var Ape = Row[2];
                        }
                    }

                    reader.Close();

                    Conn.Close();
                }

                return true;
            } 
            catch
            {
                return false;
            }
        }
        /// <summary>SAMSALCO -04-05-2015 374151 
        /// Realiza el inicio de sesion, devolviendo una entidad de tipo LoginResponse
        /// </summary>
        /// <param name="request">Argumentos de inicio de sesión</param>   
        public LoginResponse Login(LoginRequest request)
        {
            CultureConfig.SetRegionalConfiguration();
            var response = new LoginResponse();

            response.IsAuthenticated = true;
            response.AuthenticatedErrorMsg = string.Empty;

            Route newRoute = new Route() { Id = 1, Name = "Aguacatala", Description = "Aguacatala", Start = System.DateTime.Now, State = 2 };
            newRoute.Map.Add(new RouteMapPoint() { Order = 0, Latitude = 6.197776d, Longitude = -75.589886d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 1, Latitude = 6.19656d, Longitude = -75.58553d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 2, Latitude = 6.195494d, Longitude = -75.5852075d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 3, Latitude = 6.194683d, Longitude = -75.57595d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 4, Latitude = 6.194683d, Longitude = -75.57595d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 5, Latitude = 6.192546d, Longitude = -75.580013d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 6, Latitude = 6.189698d, Longitude = -75.580672d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 7, Latitude = 6.187483d, Longitude = -75.581672d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 8, Latitude = 6.185058d, Longitude = -75.583491d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 9, Latitude = 6.188546d, Longitude = -75.58587697d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 10, Latitude = 6.191597d, Longitude = -75.592221d });
            newRoute.Map.Add(new RouteMapPoint() { Order = 11, Latitude = 6.194829d, Longitude = -75.590993d });

            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 10, 0), Latitude = 6.196155, Longitude = -75.590132 });
            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 11, 0), Latitude = 6.196080, Longitude = -75.589875 });
            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 12, 0), Latitude = 6.195803, Longitude = -75.588963 });
            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 13, 0), Latitude = 6.195611, Longitude = -75.588394 });
            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 14, 0), Latitude = 6.195170, Longitude = -75.588517 });
            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 15, 0), Latitude = 6.194822, Longitude = -75.588640 });
            newRoute.TrackingDetail.Add(new TracePoint() { DateTime = new DateTime(2015, 08, 12, 8, 19, 0), Latitude = 6.194174, Longitude = -75.588753 });

            newRoute.StrucDataEntry = "<f><e id=\"1\" t=\"2\" c=\"Dirección\" v=\"\"/><e id=\"2\" t=\"3\" c=\"Productos\" v=\"Ron Medelliln|Ron Santafe|Ron Caldas\"/><e id=\"3\" t=\"1\" c=\"Tienda\" v=\"Minimercado|Billar|Cancha de Tejo\"/><e id=\"4\" t=\"6\" c=\"Comentario\" v=\"\"/></f>";

            response.Routes.Add(newRoute);

            response.User = new User() { UserId = "001", FullName = request.User, ActiveRoute = newRoute, PhotoURL = "http://www.hit4hit.org/img/login/user-icon-6.png" };

            return response;
        }

    }
}
