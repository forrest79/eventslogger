using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace EventsLogger
{

    /// <summary>
    /// Events logger service installer.
    /// </summary>
    [RunInstaller(true)]
    public class EventsLoggerServiceInstaller : Installer
    {

        /// <summary>
        /// Service process installer.
        /// </summary>
        private ServiceProcessInstaller process;

        /// <summary>
        /// Service installer.
        /// </summary>
        private ServiceInstaller service;

        /// <summary>
        /// Initialize installer.
        /// </summary>
        public EventsLoggerServiceInstaller()
        {
            process = new ServiceProcessInstaller();

            process.Account = ServiceAccount.LocalSystem;

            service = new ServiceInstaller();
            service.ServiceName = EventsLoggerService.APP;
            service.StartType = ServiceStartMode.Automatic;

            Installers.Add(process);
            Installers.Add(service);
        }
    
        /// <summary>
        /// Add starting parameter to service path.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" --service";
            base.OnBeforeInstall(savedState);
        }
    }
}
