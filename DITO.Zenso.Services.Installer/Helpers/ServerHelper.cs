using DITO.Zenso.Services.Installer.Properties;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DITO.Services.WindowsServer
{
    /// <summary>
    /// Servicios de configuracion de servidor
    /// </summary>
    public static class ServerHelper
    {
        /// <summary>
        /// Valida el sistema operativo
        /// </summary>
        public static void ValidateOS()
        {
            if (Environment.OSVersion.Version.Major >= 6 )
            {
                if (Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build >= 6002) //Windows Server 2008 SP2 - Windows Vista SP2
                    return;
                else if (Environment.OSVersion.Version.Minor >= 1) //Versiones superiores de Windows Server
                    return;
           
            }
            throw new PlatformNotSupportedException("El sistema operativo no es compatible con esta instalación. Se requiere Windows Server 2008 SP2/Windows Server 2008 R2/Window 7/Windows Vista SP2 o superior");
        }
        /// <summary>
        /// Recupera el sitio de servicios
        /// </summary>
        /// <param name="manager">Administrador de configuracion</param>
        public static Site GetServicesSite(ServerManager manager)
        {
            Site servicesSite = manager.Sites.Where(site => site.Name == Constant.DITOServicesSiteName).SingleOrDefault();
            if (servicesSite == null)
            {
                ApplicationPool pool = CreateApplicationPool(manager, Constant.DITODefaultPool);
                string sitePath = Environment.ExpandEnvironmentVariables(string.Format(@"%SystemDrive%\{0}", Constant.DITOServicesFolder));
                if (!Directory.Exists(sitePath))
                    Directory.CreateDirectory(sitePath);

                string configFileName = Path.Combine(sitePath, "web.config");
                if (!File.Exists(configFileName))
                {
                    using (FileStream configFile = new FileStream(configFileName, FileMode.Create))
                    {
                        configFile.Write(Resources.SiteWeb, 0, Resources.SiteWeb.Length);
                    }
                }
                servicesSite = manager.Sites.Add(Constant.DITOServicesSiteName, "http", "*:2080:", sitePath);
                servicesSite.Bindings.Add("808:*", "net.tcp");
                servicesSite.Bindings.Add("localhost", "net.pipe");
                servicesSite.ServerAutoStart = true;
                servicesSite.Applications[0].ApplicationPoolName = pool.Name;
            }
            return servicesSite;
        }
        /// <summary>
        /// Crea un grupo de aplicaciones
        /// </summary>
        /// <param name="name">Nombre del grupo</param>
        /// <param name="manager">Administrador de configuracion</param>
        public static ApplicationPool CreateApplicationPool(ServerManager manager, string name)
        {
            ApplicationPool appPool = manager.ApplicationPools.Where(pool => pool.Name == name).SingleOrDefault();
            if (appPool == null)
            {
                appPool = manager.ApplicationPools.Add(name);
                appPool.ManagedRuntimeVersion = "v4.0";
                appPool.AutoStart = true;                
            }
            appPool.SetAttributeValue("startMode", "AlwaysRunning");
            return appPool;
        }
        /// <summary>
        /// Crea una aplicacion
        /// </summary>
        /// <param name="name">Nombre de aplicacion</param>
        /// <param name="path">Ruta de acceso</param>
        /// <param name="enabledProtocols">Protocolos habilitados</param>
        /// <param name="binSourcePath">Ruta origen de binarios</param>
        /// <param name="installerFile">Archivo de instalacion</param>
        public static void AddApplication(string name, string path, string enabledProtocols, string binSourcePath,string installerFile)
        {
            ValidateOS();
            using (ServerManager manager = new ServerManager())
            {
                Site servicesSite = GetServicesSite(manager);
                VirtualDirectory rootDirectory = servicesSite.Applications.First().VirtualDirectories.First();
                //string applicationPath = Path.Combine(rootDirectory.PhysicalPath, name);
                //string servicePath = Path.Combine(applicationPath, path);

                string applicationPath = rootDirectory.PhysicalPath;
                string servicePath = applicationPath;

                string serviceBinPath = Path.Combine(servicePath, "bin");
                string serviceBinResourcesPath = Path.Combine(serviceBinPath, "es");
                string serviceConfigFile = Path.Combine(servicePath, "web.config");
                string virtualPath = string.Format("/{0}/{1}", name, path.Replace(@"\", "/"));
                FileInfo installationFileInfo = new FileInfo(installerFile);
                DirectoryInfo sourceDriectoryInfo = new DirectoryInfo(binSourcePath);
                DirectoryInfo resourceDirectory = new DirectoryInfo(Path.Combine(binSourcePath, "es"));

                if (!Directory.Exists(applicationPath))
                    Directory.CreateDirectory(applicationPath);

                if (!Directory.Exists(servicePath))
                    Directory.CreateDirectory(servicePath);

                if (!Directory.Exists(serviceBinPath))
                    Directory.CreateDirectory(serviceBinPath);

                if (sourceDriectoryInfo.Exists)
                {
                    FileInfo[] files = sourceDriectoryInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
                    foreach (FileInfo file in files)
                        if (file.Name != "IBM.Data.Informix.dll")
                            file.CopyTo(Path.Combine(serviceBinPath, file.Name), true);

                    if (resourceDirectory.Exists)
                    {
                        if (!Directory.Exists(serviceBinResourcesPath))
                            Directory.CreateDirectory(serviceBinResourcesPath);

                        FileInfo[] resourceFiles = resourceDirectory.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
                        foreach (FileInfo file in resourceFiles)
                            file.CopyTo(Path.Combine(serviceBinResourcesPath, file.Name), true);
                    }
                }

                if (installationFileInfo.Exists)
                {
                    XDocument installationFile = XDocument.Load(installationFileInfo.FullName);
                    List<XElement> servicesToInstall = installationFile.Descendants("Instance").ToList();
                    List<XElement> activations = new List<XElement>();
                    foreach (XElement serviceToInstal in servicesToInstall)
                    {
                        string relativeAddress = serviceToInstal.Descendants("Configuration").Elements("RelativeAddress").Single().Value;
                        string service = serviceToInstal.Descendants("Configuration").Elements("service").Single().Attribute("name").Value;
                        XElement serviceActivation = new XElement("add");
                        serviceActivation.SetAttributeValue("relativeAddress", relativeAddress);
                        serviceActivation.SetAttributeValue("service", service);
                        activations.Add(serviceActivation);
                    }
                    if (!File.Exists(serviceConfigFile))
                    {
                        using (FileStream configFile = new FileStream(serviceConfigFile, FileMode.Create))
                        {
                            configFile.Write(Resources.AppWeb, 0, Resources.AppWeb.Length);
                        }
                    }
                    XDocument webConfigFile = XDocument.Load(serviceConfigFile);
                    XElement activationsElement = webConfigFile.Descendants("system.serviceModel").Elements("serviceHostingEnvironment").Single().Element("serviceActivations");


                    List<XElement> nodes = new List<XElement>();

                    foreach (var item in activationsElement.Elements())
                    {                      
                        nodes.Add(item);
                    }
                    activationsElement.RemoveNodes();

                    foreach (var item in nodes)
                    {
                        if (!activations.Contains(item))
                        {                           
                            activations.Add(item);
                        }
                    }
                    activationsElement.Add(activations);
                    
             
                    webConfigFile.Save(serviceConfigFile);
                }

                Application application = servicesSite.Applications.Where(app => app.Path == virtualPath).FirstOrDefault();              
                if (application == null)
                {
                    application = servicesSite.Applications.Add(virtualPath, servicePath);
                    ApplicationPool pool = CreateApplicationPool(manager, string.Format("{0} App Pool", name));
                    application.ApplicationPoolName = pool.Name;
                    application.EnabledProtocols = enabledProtocols;
                }

                if (application.Attributes["serviceAutoStartEnabled"] != null)
                    application.SetAttributeValue("serviceAutoStartEnabled", "true");

                if (application.Attributes["serviceAutoStartProvider"] != null)
                    application.SetAttributeValue("serviceAutoStartProvider", "Service");

                if (application.Attributes["serviceAutoStartMode"] != null)
                    application.SetAttributeValue("serviceAutoStartMode", "All");

                manager.CommitChanges();
            }
        }
    }
}
