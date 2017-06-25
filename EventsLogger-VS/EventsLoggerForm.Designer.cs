namespace EventsLogger
{
    partial class EventsLoggerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventsLoggerForm));
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.gbService = new System.Windows.Forms.GroupBox();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblRound = new System.Windows.Forms.Label();
            this.txtRoundMinutes = new System.Windows.Forms.TextBox();
            this.lblRoundMinutes = new System.Windows.Forms.Label();
            this.gbService.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(6, 19);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(60, 30);
            this.btnInstall.TabIndex = 4;
            this.btnInstall.Text = "&Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(72, 19);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(60, 30);
            this.btnUninstall.TabIndex = 5;
            this.btnUninstall.Text = "&Uninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(155, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(60, 30);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "&Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(221, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 30);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "S&top";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(497, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(47, 22);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "O&pen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(71, 11);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(386, 20);
            this.txtDirectory.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(466, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 22);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // gbService
            // 
            this.gbService.Controls.Add(this.btnInstall);
            this.gbService.Controls.Add(this.btnUninstall);
            this.gbService.Controls.Add(this.btnStart);
            this.gbService.Controls.Add(this.btnStop);
            this.gbService.Location = new System.Drawing.Point(10, 77);
            this.gbService.Name = "gbService";
            this.gbService.Size = new System.Drawing.Size(289, 56);
            this.gbService.TabIndex = 7;
            this.gbService.TabStop = false;
            this.gbService.Text = "EventsLogger service";
            // 
            // lblDirectory
            // 
            this.lblDirectory.AutoSize = true;
            this.lblDirectory.Location = new System.Drawing.Point(13, 14);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(52, 13);
            this.lblDirectory.TabIndex = 8;
            this.lblDirectory.Text = "Directory:";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnClose.Location = new System.Drawing.Point(469, 110);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblRound
            // 
            this.lblRound.AutoSize = true;
            this.lblRound.Location = new System.Drawing.Point(12, 40);
            this.lblRound.Name = "lblRound";
            this.lblRound.Size = new System.Drawing.Size(93, 13);
            this.lblRound.TabIndex = 10;
            this.lblRound.Text = "Round time diff to:";
            // 
            // txtRoundMinutes
            // 
            this.txtRoundMinutes.Location = new System.Drawing.Point(111, 37);
            this.txtRoundMinutes.Name = "txtRoundMinutes";
            this.txtRoundMinutes.Size = new System.Drawing.Size(31, 20);
            this.txtRoundMinutes.TabIndex = 9;
            // 
            // lblRoundMinutes
            // 
            this.lblRoundMinutes.AutoSize = true;
            this.lblRoundMinutes.Location = new System.Drawing.Point(148, 40);
            this.lblRoundMinutes.Name = "lblRoundMinutes";
            this.lblRoundMinutes.Size = new System.Drawing.Size(43, 13);
            this.lblRoundMinutes.TabIndex = 11;
            this.lblRoundMinutes.Text = "minutes";
            // 
            // EventsLoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 141);
            this.Controls.Add(this.lblRoundMinutes);
            this.Controls.Add(this.lblRound);
            this.Controls.Add(this.txtRoundMinutes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblDirectory);
            this.Controls.Add(this.gbService);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EventsLoggerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EventsLogger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EventsLoggerForm_FormClosing);
            this.Load += new System.EventHandler(this.EventsLoggerForm_Load);
            this.gbService.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox gbService;
        private System.Windows.Forms.Label lblDirectory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.TextBox txtRoundMinutes;
        private System.Windows.Forms.Label lblRoundMinutes;
    }
}