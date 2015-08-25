using DITO.Zenso.Services.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DITO.Zenso.Services.TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        private const string authenticationServiceAddress = "http://localhost:2080/DITO/Services/Zenso/Authentication.svc/";
        private const string zensoAdministrationServiceAddress = "http://localhost:2080/DITO/Services/Zenso/ZensoAdministration.svc/";
        #endregion
        #region Constructor
        /// <summary>
        /// Basic Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Display Text Result On TextBox
        /// </summary>
        /// <param name="response"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private async Task DisplayTextResult(HttpResponseMessage response, TextBox output)
        {
            string responJsonText = await response.Content.ReadAsStringAsync();
            output.Text = responJsonText;
        }
        /// <summary>
        /// Gte JSON Value
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public string GetJsonValue(string jsonString)
        {
            int ValueLength = jsonString.LastIndexOf("\"") - (jsonString.IndexOf(":") + 2);
            string value = jsonString.Substring(jsonString.IndexOf(":") + 2, ValueLength);
            return value;
        }
        /// <summary>
        /// Create Service Client
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private HttpClient CreateClient(string address)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(address);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        #endregion

        #region Authentication Services
        /// <summary>
        /// Button Ping Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnPing_Click(object sender, RoutedEventArgs e)
        {
            using (var client = CreateClient(authenticationServiceAddress))
            {
                var result = await client.GetAsync("Ping");
                await DisplayTextResult(result, txtResult);
            }
        }
        /// <summary>
        /// Button Login Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var client = CreateClient(authenticationServiceAddress))
            {
                var request = new
                {
                    User = "admin",
                    Password = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("a"))
                };

                var jsonRequest = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
                var result = await client.PostAsync("Login", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

                await DisplayTextResult(result, txtResult);
            }
        }
        #endregion
        #region Zenso Services
        /// <summary>
        /// Save Trace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSaveTrace_Click(object sender, RoutedEventArgs e)
        {
            using (var client = CreateClient(zensoAdministrationServiceAddress))
            {
                var request = new SaveTraceRequest();
                request.Points.Add(new TracePoint() { DateTime = DateTime.Now, Latitude = 0.34344d, Longitude = -55.090909d });
                request.Points.Add(new TracePoint() { DateTime = DateTime.Now, Latitude = 0.786782344d, Longitude = -70.034239d });

                var jsonRequest = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
                var result = await client.PostAsync("SaveTrace", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                await DisplayTextResult(result, txtResult);
            }
        }

        /// <summary>
        /// Save Censu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSaveCensu_Click(object sender, RoutedEventArgs e)
        {
            using (var client = CreateClient(zensoAdministrationServiceAddress))
            {
                var request = new CensuPointDataRequest();
                request.UserId = "admon";
                request.RouteId = "1";
                request.CompanyCode = "01";
                request.DateTime = DateTime.Now;
                request.Data = "<f><e id=\"1\" t=\"2\" c=\"Dirección\" v=\"Cr 43A 85\"/><e id=\"2\" t=\"3\" c=\"Productos\" v=\"Ron Medelliln|Ron Santafe\"/><e id=\"3\" t=\"1\" c=\"Tienda\" v=\"Minimercado\"/><e id=\"4\" t=\"6\" c=\"Comentario\" v=\"No Apply\"/></f>";

                var jsonRequest = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
                var result = await client.PostAsync("SaveCensu", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                await DisplayTextResult(result, txtResult);
            }
        }

        /// <summary>
        /// Get Censu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGetCensu_Click(object sender, RoutedEventArgs e)
        {
            using (var client = CreateClient(zensoAdministrationServiceAddress))
            {
                string request = "1";
                var result = await client.GetAsync(string.Format("CensuData/{0}", request));
                await DisplayTextResult(result, txtResult);
            }
        }
        #endregion
    }
}
