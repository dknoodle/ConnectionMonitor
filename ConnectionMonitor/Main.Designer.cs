namespace ConnectionMonitor
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.fbdLogPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStartLogging = new System.Windows.Forms.Button();
            this.btnStopLogging = new System.Windows.Forms.Button();
            this.cbVerbose = new System.Windows.Forms.CheckBox();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gateway IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Log Path:";
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.txtHistory);
            this.gbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbLog.Location = new System.Drawing.Point(0, 93);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(593, 283);
            this.gbLog.TabIndex = 2;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "History";
            // 
            // txtHistory
            // 
            this.txtHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHistory.Location = new System.Drawing.Point(3, 16);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.ReadOnly = true;
            this.txtHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistory.Size = new System.Drawing.Size(587, 264);
            this.txtHistory.TabIndex = 0;
            // 
            // btnLogPath
            // 
            this.btnLogPath.Location = new System.Drawing.Point(538, 22);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(25, 23);
            this.btnLogPath.TabIndex = 5;
            this.btnLogPath.Text = "...";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // fbdLogPath
            // 
            this.fbdLogPath.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // btnStartLogging
            // 
            this.btnStartLogging.Location = new System.Drawing.Point(460, 64);
            this.btnStartLogging.Name = "btnStartLogging";
            this.btnStartLogging.Size = new System.Drawing.Size(103, 23);
            this.btnStartLogging.TabIndex = 6;
            this.btnStartLogging.Text = "&Start Logging";
            this.btnStartLogging.UseVisualStyleBackColor = true;
            this.btnStartLogging.Click += new System.EventHandler(this.btnStartLogging_Click);
            // 
            // btnStopLogging
            // 
            this.btnStopLogging.Location = new System.Drawing.Point(460, 64);
            this.btnStopLogging.Name = "btnStopLogging";
            this.btnStopLogging.Size = new System.Drawing.Size(103, 23);
            this.btnStopLogging.TabIndex = 7;
            this.btnStopLogging.Text = "Sto&p Logging";
            this.btnStopLogging.UseVisualStyleBackColor = true;
            this.btnStopLogging.Visible = false;
            this.btnStopLogging.Click += new System.EventHandler(this.btnStopLogging_Click);
            // 
            // cbVerbose
            // 
            this.cbVerbose.AutoSize = true;
            this.cbVerbose.Checked = global::ConnectionMonitor.Properties.Settings.Default.Verbose;
            this.cbVerbose.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ConnectionMonitor.Properties.Settings.Default, "Verbose", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbVerbose.Location = new System.Drawing.Point(249, 68);
            this.cbVerbose.Name = "cbVerbose";
            this.cbVerbose.Size = new System.Drawing.Size(65, 17);
            this.cbVerbose.TabIndex = 8;
            this.cbVerbose.Text = "Verbose";
            this.cbVerbose.UseVisualStyleBackColor = true;
            this.cbVerbose.CheckedChanged += new System.EventHandler(this.cbVerbose_CheckedChanged);
            // 
            // txtLogPath
            // 
            this.txtLogPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ConnectionMonitor.Properties.Settings.Default, "LogPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtLogPath.Location = new System.Drawing.Point(249, 25);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.ReadOnly = true;
            this.txtLogPath.Size = new System.Drawing.Size(283, 20);
            this.txtLogPath.TabIndex = 4;
            this.txtLogPath.Text = global::ConnectionMonitor.Properties.Settings.Default.LogPath;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ConnectionMonitor.Properties.Settings.Default, "DefaultGatewayIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(15, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(162, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = global::ConnectionMonitor.Properties.Settings.Default.DefaultGatewayIP;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 376);
            this.Controls.Add(this.cbVerbose);
            this.Controls.Add(this.btnStartLogging);
            this.Controls.Add(this.btnLogPath);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStopLogging);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Monitor";
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtHistory;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.FolderBrowserDialog fbdLogPath;
        private System.Windows.Forms.Button btnStartLogging;
        private System.Windows.Forms.Button btnStopLogging;
        private System.Windows.Forms.CheckBox cbVerbose;
    }
}

