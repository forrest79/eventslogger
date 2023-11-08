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
        private const string KEY = "SOFTWARE\\Forrest79\\" + EventsLoggerService.APP;

        /// <summary>
        /// Is settings loaded.
        /// </summary>
        private bool isLoaded = false;

        /// <summary>
        /// Directory with logs.
        /// </summary>
        private string logDirectory = "";

        /// <summary>
        /// Round time diff to minutes.
        /// </summary>
        private int roundMinutes = 0;

        /// <summary>
        /// Load settings if neccesary
        /// </summary>
        private void Load()
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

                int tempRoundMinutes;
                if (Int32.TryParse(((string)rk.GetValue("roundMinutes")).TrimEnd(slash), out tempRoundMinutes))
                {
                    if ((tempRoundMinutes >= 0) && (tempRoundMinutes <= 60))
                    {
                        roundMinutes = tempRoundMinutes;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Save settings to registry.
        /// </summary>
        private void Save()
        {
            RegistryKey rk = Registry.LocalMachine.CreateSubKey(KEY);
            rk.SetValue("logDirectory", logDirectory);
            rk.SetValue("roundMinutes", roundMinutes.ToString());
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
        /// <returns>Settings.</returns>
        public Settings SetLogDirectory(string logDirectory)
        {
            this.logDirectory = logDirectory;

            Save();

            return this;
        }

        /// <summary>
        /// Get round time diff to minutes.
        /// </summary>
        /// <returns>Round minutes.</returns>
        public int GetRoundMinutes()
        {
            Load();

            return roundMinutes;
        }

        /// <summary>
        /// Set round time diff to minutes.
        /// </summary>
        /// <param name="logDirectory">Round time diff to minutes.</param>
        /// <returns>Settings.</returns>
        public Settings SetRoundMinutes(int roundMinutes)
        {
            this.roundMinutes = roundMinutes;

            Save();

            return this;
        }
    }
}
