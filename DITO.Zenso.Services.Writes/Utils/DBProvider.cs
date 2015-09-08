using System;
using System.Configuration;
using System.Data;
using System.Xml;

namespace DITO.Zenso.Services.Writes
{
    /// <summary>
    /// Get DB Provider
    /// </summary>
    public static class DBProvider
    {   
        /// <summary>
        /// Get Connection String
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static String GetConectionString(string connectionName)
        {            
            string connectionString = string.Empty;

            string defaultDatabase = String.IsNullOrEmpty(connectionName) ? 
                ConfigurationManager.AppSettings.Get("defaultDatabase") : connectionName;

            if (defaultDatabase != null)
            {
                ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings[defaultDatabase];
                if (connection != null)
                    connectionString = connection.ConnectionString;
                else 
                    throw new ArgumentNullException("ConnectionString", string.Format("La cadena de conexion '{0}' no existe en el archivo de configuración", defaultDatabase));
            }
            else
            {
                string dataConfiguration;

                var ds = ReadAppConfig();

                if (!String.IsNullOrEmpty(connectionName))
                    dataConfiguration = connectionName;
                else
                    dataConfiguration = ds.Tables["dataConfiguration"].Rows[0]["Value"].ToString();

                DataRow[] connections = ds.Tables["connectionStrings"].Select(String.Format("Item='{0}'", dataConfiguration));

                if (connections.Length > 0)
                {
                    connectionString = connections[0]["Value"].ToString();
                }
            }

            return connectionString;
        }

        /// <summary>
        /// Lee el archivo de configuración
        /// </summary>
        /// <returns></returns>
        public static DataSet ReadAppConfig()
        {
            var xmlDoc = new XmlDocument();
            string keyName = String.Empty, keyValue = String.Empty, defaultDatabase = String.Empty;
            var ds = new DataSet();

            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement xElement in xmlDoc.DocumentElement)
            {
                ds.Tables.Add(xElement.Name);

                ds.Tables[xElement.Name].Columns.Add("Item");
                ds.Tables[xElement.Name].Columns.Add("Value");

                if (xElement.Name == "dataConfiguration")
                {
                    keyName = "defaultDatabase";
                    defaultDatabase = xElement.Attributes[keyName].Value;

                    if (!String.IsNullOrEmpty(defaultDatabase))
                        ds.Tables[xElement.Name].Rows.Add(new object[] { keyName, defaultDatabase });
                }
                else if (xElement.Name == "connectionStrings")
                {
                    ds.Tables[xElement.Name].Columns.Add("providerName");
                    string providerName = String.Empty;

                    foreach (XmlNode xNode in xElement.ChildNodes)
                    {
                        if (xNode.NodeType != XmlNodeType.Element)
                            continue;

                        keyName = xNode.Attributes["name"].Value;
                        keyValue = xNode.Attributes["connectionString"].Value;
                        providerName = xNode.Attributes["providerName"].Value;

                        ds.Tables[xElement.Name].Rows.Add(new object[] { keyName, keyValue, providerName });
                    }
                }                
            }

            return ds;
        }
    }
}
