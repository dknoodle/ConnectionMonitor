using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace ConnectionMonitor
{
    public partial class Main : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        private static StringBuilder logdata = new StringBuilder();
        private static bool verbose;
        private static string logpath;

        System.Threading.Thread bt = new System.Threading.Thread(BackgroundFlush);
        
        private enum ConnectionStatus : int
        {
            Down = 1,
            Up = 2,
            Unknown = 3
        }

        public Main()
        {
            InitializeComponent();

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            verbose = this.cbVerbose.Checked;
        }

        private void btnStartLogging_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();

            if (RunReady())
            {
                logpath = Properties.Settings.Default.LogPath;
                string log = string.Format("Logging Started: {0}\n", DateTime.Now);

                this.txtHistory.Text = log;
                logdata.AppendLine(log);

                bw.RunWorkerAsync();

                this.btnStartLogging.Visible = false;
                this.btnStopLogging.Visible = true;

                this.txtHistory.ReadOnly = true;
                this.nudFlush.ReadOnly = true;

                bt.IsBackground = true;
                bt.Start();

            }
        }

        private bool RunReady()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DefaultGatewayIP))
            {
                MessageBox.Show("The Gateway IP to ping is required.  Please set the IP address and try again.");
                return false;
            }
            else
            {
                try
                {
                    IPAddress address = IPAddress.Parse(Properties.Settings.Default.DefaultGatewayIP);
                }
                catch
                {
                    MessageBox.Show("The IP address entered is invalid.  Please enter a valid IP address in the format xxx.xxx.xxx.xxx and try again.");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LogPath))
            {
                MessageBox.Show("Please select a log path to ouput log information and try again.");
                return false;
            }
            else
            {
                try
                {
                    //Check to see if the log path exists and create it if needed.
                    if (!Directory.Exists(Properties.Settings.Default.LogPath)) { Directory.CreateDirectory(Properties.Settings.Default.LogPath); }
                }
                catch
                {
                    MessageBox.Show("The log path you entered is invalid.  Please select a valid directory path and try again.");
                    return false;
                }
            }


            return true;
        }

        private void btnStopLogging_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
            bt.Abort();

            this.btnStopLogging.Visible = false;
            this.btnStartLogging.Visible = true;

            this.txtHistory.ReadOnly = false;
            this.nudFlush.ReadOnly = false;
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {
            this.fbdLogPath.ShowDialog();

            if (!string.IsNullOrWhiteSpace(this.fbdLogPath.SelectedPath))
            {
                this.txtLogPath.Text = this.fbdLogPath.SelectedPath;
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            bool firstrun = true;

            DateTime downtimestart = DateTime.Now;
            DateTime downtimeend = DateTime.Now;

            ConnectionStatus laststatus = ConnectionStatus.Unknown;

            int count = 0;
            long total = 0;

            // Only keep the last 50 pings in the avg
            if (count >= 50)
            {
                total = total / count;
                count = 1;
            }

            while (!worker.CancellationPending)
            {
                StringBuilder sbHistory = new StringBuilder();
                StringBuilder sbLog = new StringBuilder();
                ProgressData pd = new ProgressData();
                string saddress = "76.185.224.1";

                Ping pingSender = new Ping();

                try
                {
                    IPAddress address = IPAddress.Parse(saddress);
                    PingReply reply = pingSender.Send(address, 5000);

                    count++;
                    total = total + reply.RoundtripTime;

                    if (reply.Status == IPStatus.Success)
                    {
                        if (firstrun)
                        {
                            sbHistory.AppendLine("Connection Status: Up");
                            sbLog.AppendLine("Connection Status: Up");
                            firstrun = false;
                        }

                        if (laststatus == ConnectionStatus.Down)
                        {
                            downtimeend = DateTime.Now;
                            var span = downtimeend - downtimestart;

                            string status = string.Format("Connection Up: {0}    Downtime: {1} hours - {2} minutes - {3} seconds", DateTime.Now, span.Hours, span.Minutes, span.Seconds);
                            sbLog.AppendLine(status);
                            sbHistory.AppendLine(status);
                        }

                        laststatus = ConnectionStatus.Up;

                        if (verbose) { sbHistory.AppendLine(string.Format("Time: {0}     Address: {1}     Status: {2}     RoundTrip: {3} ms     Average: {4} ms", DateTime.Now, saddress, reply.Status, reply.RoundtripTime, total / count)); }
                    }
                    else
                    {
                        firstrun = false;
                        count = 0;
                        total = 0;

                        if (laststatus != ConnectionStatus.Down)
                        {
                            downtimestart = DateTime.Now;

                            string status = string.Format("Connection Down: {0}", DateTime.Now);
                            sbLog.AppendLine(status);
                            sbHistory.AppendLine(status);
                        }

                        laststatus = ConnectionStatus.Down;

                        if (verbose) { sbHistory.AppendLine(string.Format("Time: {0}     Address: {1}     Status: {2}     RoundTrip: {3} ms", DateTime.Now, saddress, reply.Status, reply.RoundtripTime)); }
                    }
                }
                catch (PingException pe)
                {
                    sbHistory.AppendLine(pe.Message);
                }
                catch (NetworkInformationException net)
                {
                    sbHistory.AppendLine(net.Message);
                }
                catch (Exception ex)
                {
                    sbHistory.AppendLine(ex.Message);
                }
                finally
                {
                    pingSender.Dispose();
                }

                if (sbHistory.Length > 0) { pd.History = sbHistory.ToString(); }
                if (sbLog.Length > 0) { pd.Log = sbLog.ToString(); }

                worker.ReportProgress(0, pd);

                sbHistory = null;
                sbLog = null;

                System.Threading.Thread.Sleep(3000);
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string log = string.Format("\nLogging Stopped: {0}", DateTime.Now);

            this.txtHistory.AppendText(log);
            logdata.AppendLine(log);

            FlushLog();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressData pd =  (ProgressData)e.UserState;

            logdata.AppendLine(pd.Log);
            this.txtHistory.AppendText(pd.History);
        }

        private static void BackgroundFlush()
        {
            while (true)
            {
                int count = Convert.ToInt32(Properties.Settings.Default.FlushMin * 60000);
                System.Threading.Thread.Sleep(count);
                FlushLog();
            }
        }

        private static void FlushLog()
        {
            try
            {
                if (logdata.ToString().Length > 0)
                {
                    string path = string.Format(@"{0}\ConnectionMonitorLog.txt", logpath);

                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.Write(logdata.ToString());
                            logdata = new StringBuilder();
                        }
                    }
                    else
                    {
                        // Append log information
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.Write(logdata.ToString());
                            logdata = new StringBuilder();
                        }
                    }
                }
            }
            finally { }
        }

        private void cbVerbose_CheckedChanged(object sender, EventArgs e)
        {
            verbose = this.cbVerbose.Checked;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            FlushLog();
            Properties.Settings.Default.Save();
            base.OnFormClosing(e);
        }
    }
}
