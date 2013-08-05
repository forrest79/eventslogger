using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace EventsLogger
{

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {

        /// <summary>
        /// Choose if run service or control form.
        /// </summary>
        static void Main(string[] args)
        {
            bool startService = false;

            for (int i = 0; i < args.Length; i++)
            {
                if ((args[i].ToLower() == "--service") || (args[i].ToLower() == "-s"))
                {
                    startService = true;
                    break;
                }
            }

            if (startService)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			{ 
				new EventsLoggerService() 
			};
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new EventsLoggerForm());
            }
        }
    }
}
