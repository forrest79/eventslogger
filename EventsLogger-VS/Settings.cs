using System;
using Microsoft.Win32;

namespace EventsLogger
{

    /// <summary>
    /// Settings in Windows registry.
    /// </summary>
    public class Settings
    {

        /// <summary>
        /// Registry key.
        /// </summary>
        protected const string KEY = "SOFTWARE\\Forrest79.net\\" + EventsLoggerService.APP;

        /// <summary>
        /// Is settings loaded.
        /// </summary>
        protected bool isLoaded = false;

        /// <summary>
        /// Directory with logs.
        /// </summary>
        protected string logDirectory = "";

        /// <summary>
        /// Load settings if neccesary
        /// </summary>
        protected void Load()
        {
            if (isLoaded)
            {
                return;
            }

            RegistryKey rk = Registry.LocalMachine.OpenSubKey(KEY);
            
            if (rk == null)
            {
                return;
            }

            try
            {
                char[] slash = {'\\'};
                logDirectory = ((string)rk.GetValue("logDirectory")).TrimEnd(slash);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Save settings to registry.
        /// </summary>
        protected void Save()
        {
            RegistryKey rk = Registry.LocalMachine.CreateSubKey(KEY);
            rk.SetValue("logDirectory", logDirectory);
        }

        /// <summary>
        /// Get directory for logs.
        /// </summary>
        /// <returns>Logs directory.</returns>
        public string GetLogDirectory()
        {
            Load();

            return logDirectory;
        }

        /// <summary>
        /// Set directory for logs.
        /// </summary>
        /// <param name="logDirectory">Logs directory.</param>
        /// <returns>Settigns.</returns>
        public Settings SetLogDirectory(string logDirectory)
        {
            this.logDirectory = logDirectory;

            Save();

            return this;
        }
    }
}
