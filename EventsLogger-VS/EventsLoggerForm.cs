using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace EventsLogger
{
    /// <summary>
    /// EventsLogger control form.
    /// </summary>
    public partial class EventsLoggerForm : Form
    {

        /// <summary>
        /// Settings.
        /// </summary>
        protected Settings settings;

        /// <summary>
        /// Initialize form.
        /// </summary>
        public EventsLoggerForm()
        {
            InitializeComponent();
            settings = new Settings();
        }

        /// <summary>
        /// Load form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventsLoggerForm_Load(object sender, EventArgs e)
        {
            txtDirectory.Text = settings.GetLogDirectory();
            CheckStatus();
        }

        /// <summary>
        /// Handle browse button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string selectedPath = "";
            var t = new Thread((ThreadStart)(() =>
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                selectedPath = fbd.SelectedPath;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            txtDirectory.Text = selectedPath;
        }

        /// <summary>
        /// Handle open button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (CheckDirectory())
            {
                Process.Start(txtDirectory.Text);
            }
        }

        /// <summary>
        /// Handle close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle install button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstall_Click(object sender, EventArgs e)
        {
            EventsLoggerService.ServiceInstall();
        }

        /// <summary>
        /// Handle uninstall button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUninstall_Click(object sender, EventArgs e)
        {
            EventsLoggerService.ServiceUninstall();
        }

        /// <summary>
        /// Handle start button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            EventsLoggerService.ServiceStart();
        }

        /// <summary>
        /// Handle stop button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            EventsLoggerService.ServiceStop();
        }

        /// <summary>
        /// Check service status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            CheckStatus();
        }

        /// <summary>
        /// Check directory, save settings and restart service if necessary before close form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventsLoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckDirectory())
            {
                e.Cancel = true;
            }
            else
            {
                if (settings.GetLogDirectory() != txtDirectory.Text)
                {
                    settings.SetLogDirectory(txtDirectory.Text);

                    if (EventsLoggerService.ServiceIsRunning())
                    {
                        EventsLoggerService.ServiceRestart();
                    }
                }

                Application.Exit();
            }
        }

        /// <summary>
        /// Check if directory exists and show error if not.
        /// </summary>
        /// <returns>True if directory exists, false otherwise.</returns>
        protected bool CheckDirectory()
        {
            if ((txtDirectory.Text.Length > 0) && !Directory.Exists(txtDirectory.Text))
            {
                MessageBox.Show("Directory '" + txtDirectory.Text + "' does not exists", "Directory doest not exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check service status and enable buttons.
        /// </summary>
        protected void CheckStatus()
        {
            if (EventsLoggerService.ServiceIsInstalled())
            {
                btnInstall.Enabled = false;
                btnUninstall.Enabled = true;

                if (EventsLoggerService.ServiceIsRunning()) 
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
            else
            {
                btnInstall.Enabled = true;
                btnUninstall.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = false;
            }
        }
    }
}
