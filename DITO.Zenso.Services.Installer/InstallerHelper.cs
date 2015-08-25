using DITO.Services.WindowsServer;
using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DITO.Zenso.Services.Installer
{
    /// <summary>
    /// Automatización de las operaciones de instalación
    /// </summary>
    [RunInstaller(true)]
    public partial class InstallerHelper : System.Configuration.Install.Installer
    {
        const string serverInstallationArg = "ServerType";
        const string installationPathArg = "TargetDir";
        const string installationTypeArg = "InstallOn";

        const string serviceDefinitionFile = "DITO.Services.wcfi";
        const string appPoolName = "DITO";
        const string iisVirtualDir = "Services\\Zenso";
        const string serverInstallation = "Server";

        public InstallerHelper()
        {
            InitializeComponent();
        }

        /// <summary>Evento lanzado al inicio de la instalación</summary>
        /// <param name="stateSaver">Estado de instalacion</param>
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            if (Context.Parameters.ContainsKey(serverInstallationArg))
            {
                string serverType = Context.Parameters[serverInstallationArg];
                stateSaver.Add(serverInstallationArg, serverType);
            }
        }
        /// <summary>Evento que se lanza al completar la instalacion</summary>
        /// <param name="savedState">Variables de estado</param>
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            string installationPath = Context.Parameters[installationPathArg];
            string windowsDir = Environment.GetEnvironmentVariable("windir");

            if (installationPath.EndsWith(@"\\")) installationPath = installationPath.Replace(@"\\", @"\");
            if (!installationPath.EndsWith(@"\")) installationPath += @"\";
            if (!windowsDir.EndsWith(@"\")) windowsDir += @"\";

            if (Context.Parameters[installationTypeArg] == serverInstallation)
            {
                string servicesInstallationPath = installationPath + @"ServiceInstaller\";
                string ditoServicesPath = string.Format(@"%SystemDrive%\{0}", DITO.Services.WindowsServer.Constant.DITOServicesFolder);
                
                string wasPath = Environment.ExpandEnvironmentVariables(ditoServicesPath);
                string wcfiFile = Path.Combine(servicesInstallationPath, serviceDefinitionFile);

                // Publicar el servicio en IIS
                ServerHelper.AddApplication(appPoolName, iisVirtualDir, "http", installationPath, wcfiFile);

                // Reemplazar el Web.Config para exponer el EndPoint de JSON
                string webConfigSource = Path.Combine(servicesInstallationPath, "web.Config");
                string webConfigTarget = Path.Combine(wasPath, "web.Config");

                File.Copy(webConfigSource, webConfigTarget, true);
            }
        }

        /// <summary>Evento que se lanza al completar la desinstalacion</summary>
        /// <param name="savedState">Variables de estado</param>
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
    }
}
