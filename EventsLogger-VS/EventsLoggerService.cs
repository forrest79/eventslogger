using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;
using System.Configuration.Install;
using System.Reflection;

namespace EventsLogger
{

    /// <summary>
    /// Events logger service.
    /// </summary>
    public partial class EventsLoggerService : ServiceBase
    {

        /// <summary>
        /// Service name.
        /// </summary>
        public const string APP = "EventsLogger";

        /// <summary>
        /// Service name.
        /// </summary>
        protected const int LINE_WIDTH = 65;

        /// <summary>
        /// Settings.
        /// </summary>
        protected Settings settings;

        /// <summary>
        /// Directory with logs.
        /// </summary>
        protected string logDirectory = "";

        /// <summary>
        /// Time for last event.
        /// </summary>
        protected DateTime lastEventTime;

        /// <summary>
        /// Initialize service.
        /// </summary>
        public EventsLoggerService()
        {
            InitializeComponent();

            ServiceName = APP;

            CanHandlePowerEvent = true;
            CanHandleSessionChangeEvent = true;
        }

        /// <summary>
        /// Start service.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            settings = new Settings();
            logDirectory = settings.GetLogDirectory();

            if (logDirectory.Length == 0)
            {
                WriteToEventLog("Can't log events - directory for logger is not set.", EventLogEntryType.Error);
            }

            WriteToLog("EVENTS LOGGER START");
        }

        /// <summary>
        /// Stop service.
        /// </summary>
        protected override void OnStop()
        {
            WriteToLog("EVENTS LOGGER STOP");

            settings = null;
            logDirectory = "";
        }

        /// <summary>
        /// Pause service.
        /// </summary>
        protected override void OnPause()
        {
            WriteToLog("EVENTS LOGGER PAUSED");
        }

        /// <summary>
        /// Continue service.
        /// </summary>
        protected override void OnContinue()
        {
            WriteToLog("EVENTS LOGGER CONTINUE");
        }

        /// <summary>
        /// Shutdown computer.
        /// </summary>
        protected override void OnShutdown()
        {
            WriteToLog("COMPUTER SHUTDOWN", true);
        }

        /// <summary>
        /// Custom command.
        /// </summary>
        /// <param name="command"></param>
        protected override void OnCustomCommand(int command)
        {
            WriteToLog("CUSTOM COMMAND [#" + command.ToString() + "]");
        }

        /// <summary>
        /// Power event.
        /// </summary>
        /// <param name="powerStatus"></param>
        /// <returns></returns>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            string eventInfo;
            bool highlight = false;

            switch (powerStatus)
            {
                case PowerBroadcastStatus.BatteryLow:
                    eventInfo = "Low battery";
                    break;

                case PowerBroadcastStatus.OemEvent :
                    eventInfo = "Oem";
                    break;

                case PowerBroadcastStatus.PowerStatusChange :
                    eventInfo = "Change power status";
                    break;

                case PowerBroadcastStatus.QuerySuspend :
                    eventInfo = "Query suspend";
                    break;

                case PowerBroadcastStatus.QuerySuspendFailed :
                    eventInfo = "Failed query suspend";
                    break;

                case PowerBroadcastStatus.ResumeAutomatic :
                    eventInfo = "Automatic resume";
                    break;

                case PowerBroadcastStatus.ResumeCritical :
                    eventInfo = "Critical resume";
                    break;

                case PowerBroadcastStatus.ResumeSuspend :
                    eventInfo = "Suspend resume";
                    break;

                case PowerBroadcastStatus.Suspend :
                    eventInfo = "Suspend";
                    highlight = true;
                    break;

                default :
                    eventInfo = "N/A";
                    break;

            }

            WriteToLog("POWER EVENT [" + eventInfo + "]", highlight);
            return true;
        }

        /// <summary>
        /// Session change event.
        /// </summary>
        /// <param name="changeDescription"></param>
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            WriteToLog("SESSION CHANGE [" + changeDescription.Reason.ToString() + ((changeDescription.SessionId > 1) ? (" #" + changeDescription.SessionId.ToString()) : "") + "]", changeDescription.Reason == SessionChangeReason.SessionUnlock);
        }

        /// <summary>
        /// Write to text log file.
        /// </summary>
        /// <param name="message">Message to log.</param>
        protected void WriteToLog(string message)
        {
            WriteToLog(message, false);
        }

        /// <summary>
        /// Write to text log file.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="highlightPreviousTime">Message to log.</param>
        protected void WriteToLog(string message, bool highlightPreviousTime)
        {
            if (logDirectory.Length == 0)
            {
                return;
            }

            DateTime eventTime = DateTime.Now;

            message = eventTime.ToString("yyyy-MM-dd HH:mm:ss") + ": " + message;
            if (lastEventTime != DateTime.MinValue)
            {
                TimeSpan timeDiff = eventTime - lastEventTime;
                message += (new String(' ', ((LINE_WIDTH - message.Length) > 0) ? (LINE_WIDTH - message.Length) : 0)) + (highlightPreviousTime ? "=" : "-") + "> from last event: " + timeDiff.ToString(((timeDiff.Days > 0) ? @"d\." : "") + @"hh\:mm\:ss");
            }

            lastEventTime = eventTime;

            string path = logDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            StreamWriter sw = null;
            try
            {
                if (!File.Exists(path))
                {
                    sw = File.CreateText(path);
                }
                else
                {
                    sw = File.AppendText(path);
                }

                sw.WriteLine(message);
            }
            catch (Exception e)
            {
                WriteToEventLog(e.Message, EventLogEntryType.Error);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                    
            }
        }

        /// <summary>
        /// Write to Windows event log.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="type">Message type.</param>
        protected void WriteToEventLog(string message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(APP))
            {
                EventLog.CreateEventSource(APP, "Application");
            }
            
            EventLog.WriteEntry(APP, message, type);
        }

        /// <summary>
        /// Check if events logger service is already installed.
        /// </summary>
        /// <returns>True if service is installed, false otherwise.</returns>
        public static bool ServiceIsInstalled()
        {
            return (GetService() == null) ? false : true;
        }

        /// <summary>
        /// Check if events logger service is already running.
        /// </summary>
        /// <returns>True if service is running, false otherwise.</returns>
        public static bool ServiceIsRunning()
        {
            ServiceController sc = GetService();
            if ((sc != null) && (sc.Status == ServiceControllerStatus.Running))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Install events logger service.
        /// </summary>
        public static void ServiceInstall()
        {
            if (!ServiceIsInstalled())
            {
                ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
            }
            else
            {
                throw new InstallException("Service is already installed.");
            }
        }

        /// <summary>
        /// Uninstall events logger service.
        /// </summary>
        public static void ServiceUninstall()
        {
            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
        }

        /// <summary>
        /// Start events logger service.
        /// </summary>
        public static void ServiceStart()
        {
            ServiceController sc = GetService();
            if ((sc != null) && (sc.Status == ServiceControllerStatus.Stopped))
            {
                sc.Start();
            }
            else
            {
                throw new InstallException("Service is not installed or not sttoped.");
            }
        }

        /// <summary>
        /// Stop events logger service.
        /// </summary>
        public static void ServiceStop()
        {
            ServiceController sc = GetService();
            if ((sc != null) && (sc.Status == ServiceControllerStatus.Running))
            {
                sc.Stop();
            }
            else
            {
                throw new InstallException("Service is not installed or not running.");
            }
        }

        /// <summary>
        /// Restart events logger service.
        /// </summary>
        public static void ServiceRestart()
        {
            ServiceController sc = GetService();
            if ((sc != null) && (sc.Status == ServiceControllerStatus.Running))
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(10000);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            else
            {
                throw new InstallException("Service is not installed or not running.");
            }
        }

        /// <summary>
        /// Return events logger service, if installed.
        /// </summary>
        /// <returns>Events logger service.</returns>
        protected static ServiceController GetService()
        {
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                if (sc.ServiceName == APP)
                {
                    return sc;
                }
            }

            return null;
        }
    }
}
