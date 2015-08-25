using System;
using System.Collections.Generic;
using System.Data.Common;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using System.Xml;

namespace DITO.Zenso.Services.TestHost
{
    /// <summary>
    /// Test Services Host Server
    /// </summary>
    public partial class Host : Form
    {
        #region Internals
        Dictionary<string, ServiceHost> HostSevices;
        ServiceDebugBehavior debugBehavior;
        #endregion
        #region Constructor
        /// <summary>
        /// Basic Constructor
        /// </summary>
        public Host()
        {
            InitializeComponent();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Load Configuration and Services
        /// </summary>
        public void LoadConfigAndService()
        {
            try
            {
                rtbOutput.AppendText(Environment.NewLine);

                //var dbLink = GetDefaultDatabase();

                //rtbOutput.AppendText(string.Format("Base de Datos: {0}", Environment.NewLine));
                //rtbOutput.AppendText(dbLink.Key);

                //using (var connection = GetConnection(dbLink.Value, dbLink.Key))
                //    rtbOutput.AppendText(string.Format("- (OK) {0}", Environment.NewLine));

                //rtbOutput.AppendText(Environment.NewLine);

                HostSevices = new Dictionary<string, ServiceHost>();

                rtbOutput.AppendText("Servicios: ");
                rtbOutput.AppendText(Environment.NewLine);

                // Matricular los servicios
                HostSevices.Add("Authentication.svc", new ServiceHost(typeof(DITO.Zenso.Services.Writes.Authentication)));
                HostSevices.Add("ZensoAdministration.svc", new ServiceHost(typeof(DITO.Zenso.Services.Writes.ZensoAdministration)));

                foreach (KeyValuePair<string, ServiceHost> host in HostSevices)
                {
                    // Iniciar los servicios
                    host.Value.Open();
                    rtbOutput.AppendText(string.Format("{0}{1}", String.Format("{0} Online.", host.Key), Environment.NewLine));

                    // Asignar comportamiento de debug
                    debugBehavior = host.Value.Description.Behaviors.Find<ServiceDebugBehavior>();

                    if (debugBehavior == null)
                    {
                        debugBehavior = new ServiceDebugBehavior
                        {
                            HttpHelpPageEnabled = false,
                            HttpHelpPageUrl = new Uri("http://www.dito.com.co"),
                            IncludeExceptionDetailInFaults = true
                        };

                        host.Value.Description.Behaviors.Add(debugBehavior);
                    }
                }
            }
            catch (Exception ex)
            {
                rtbOutput.AppendText(string.Format("Error: {0}{1}", ex.GetType(), Environment.NewLine));
                rtbOutput.AppendText(string.Format("Mensaje: {0}{1}", ex.Message, Environment.NewLine));

                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }
        /// <summary>
        /// Close Channel
        /// </summary>
        private void CloseChannel()
        {
            if (HostSevices != null && HostSevices.Count > 0)
            {
                rtbOutput.AppendText(Environment.NewLine);

                foreach (KeyValuePair<string, ServiceHost> host in HostSevices)
                {
                    // Detener los servicios
                    host.Value.Close();
                    rtbOutput.AppendText(string.Format("{0} Off-Line...{1}", host.Key, Environment.NewLine));
                }

                HostSevices.Clear();
            }
        }
        /// <summary>
        /// Administrador general de los eventos de botones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralActionClickHandler(object sender, EventArgs e)
        {
            var menuButton = sender is ToolStripMenuItem ? (ToolStripMenuItem)sender : null;
            var toolButton = sender is ToolStripButton ? (ToolStripButton)sender : null;
            var window = sender is Form ? (Form)sender : null;

            if (toolButton != null && toolButton == tsbExit)
            {
                CloseChannel();
                Application.Exit();
            }
            else if (toolButton != null && toolButton == tsbClean)
            {
                rtbOutput.Text = string.Empty;
            }
            else if (toolButton != null && toolButton == tsbStop)
            {
                CloseChannel();
            }
            else if (toolButton != null && toolButton == tsbStart)
            {
                if (HostSevices.Count > 0)
                    return;

                LoadConfigAndService();
            }
            else if (window != null)
            {
                if (e is FormClosedEventArgs)
                    CloseChannel();
                else
                    LoadConfigAndService();
            }
        }
        /// <summary>
        /// Get Factory Connection From Config File
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static DbConnection GetConnection(string providerName, string connectionString)
        {
            DbConnection connection = null;

            try
            {
                var factory = DbProviderFactories.GetFactory(providerName);
                connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                if (providerName == "IBM.Data.Informix")
                {
                    const string informixAssembly = "IBM.Data.Informix, Version=3.0.0.2, Culture=neutral, PublicKeyToken=7c307b91aa13d208";
                    const string informixClass = "IBM.Data.Informix.IfxConnection";

                    System.Reflection.Assembly gacAssembly = System.Reflection.Assembly.Load(informixAssembly);
                    connection = (DbConnection)gacAssembly.CreateInstance(informixClass);
                }
                else
                    throw;
            }

            if (connection != null)
            {
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            else
                throw new ApplicationException("No se pudo crear el objeto conexión :-(");

            return connection;
        }
        /// <summary>
        /// Get Default DataBase
        /// </summary>
        /// <returns></returns>
        private static KeyValuePair<string, string> GetDefaultDatabase()
        {
            string configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            string database = configFile;
            string providerName = null;

            XmlDocument root = new XmlDocument();
            root.Load(configFile);

            XmlNode dataConfigurationNode = root.SelectSingleNode("configuration/dataConfiguration");

            if (dataConfigurationNode != null)
            {
                database = dataConfigurationNode.Attributes["defaultDatabase"].InnerText;

                if (!string.IsNullOrEmpty(database))
                {
                    string query = string.Format("configuration/connectionStrings/add[@name = '{0}']", database);
                    XmlNodeList nodes = root.SelectNodes(query);

                    if (nodes.Count > 0)
                    {
                        database = nodes[0].Attributes["connectionString"].InnerText;
                        providerName = nodes[0].Attributes["providerName"].InnerText;
                    }
                }
            }

            return new KeyValuePair<string, string>(database, providerName);
        }
        #endregion
    }
}
