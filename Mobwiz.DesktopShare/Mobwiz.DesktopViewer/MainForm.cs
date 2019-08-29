using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxRDPCOMAPILib;

namespace Mobwiz.DesktopViewer
{
    public partial class MainForm : Form
    {
        private HostFinder _hostFinder;
        private bool _isConnected = false;

        public MainForm()
        {
            InitializeComponent();
#if !DEBUG
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
#endif

            _hostFinder = new HostFinder();
            _hostFinder.HostFound += HostFinderOnHostFound;
            _hostFinder.StartFind();

            axRDPViewer1.OnConnectionEstablished += AxRdpViewer1OnOnConnectionEstablished;
            axRDPViewer1.OnConnectionFailed += AxRdpViewer1OnOnConnectionFailed;
            axRDPViewer1.OnConnectionTerminated += AxRdpViewer1OnOnConnectionTerminated;
            axRDPViewer1.OnError += AxRdpViewer1OnOnError;

            notifyIcon1.ShowBalloonTip(1000, "启动成功", "远程桌面广播客户端已启动", ToolTipIcon.Info);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void AxRdpViewer1OnOnError(object sender, _IRDPSessionEvents_OnErrorEvent irdpSessionEventsOnErrorEvent)
        {
            //throw new NotImplementedException();
        }

        private void AxRdpViewer1OnOnConnectionTerminated(object sender, _IRDPSessionEvents_OnConnectionTerminatedEvent irdpSessionEventsOnConnectionTerminatedEvent)
        {
            Debug.WriteLine("Connectoin Terminated");
            _isConnected = false;
            this.Invoke(new Action(() =>
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "连接停止", "远程桌面广播已停止", ToolTipIcon.Info);
            }));
        }

        private void AxRdpViewer1OnOnConnectionFailed(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("Connectoin failed");
            _isConnected = false;
            this.Invoke(new Action(() =>
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "连接失败", "远程桌面广播连接失败", ToolTipIcon.Error);
            }));

            
        }

        private void AxRdpViewer1OnOnConnectionEstablished(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("Connectoin established");
            _isConnected = true;

            this.Invoke(new Action(() =>
            {
                //this.TopMost = true;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.Hide();
                this.Show();
                notifyIcon1.ShowBalloonTip(1000, "连接成功", "远程桌面广播连接成功", ToolTipIcon.Info);
            }));
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void HostFinderOnHostFound(object sender, HostFoundEventArgs args)
        {
            try
            {
                if (args.IsCasting)
                {
                    if (!_isConnected)
                    {
                        axRDPViewer1.SmartSizing = true;
                        axRDPViewer1.Connect(args.ConnectionString, Environment.UserName, "");
                    }
                }
                else if (!args.IsCasting)
                {
                    if (_isConnected)
                    {
                        axRDPViewer1.Disconnect();
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception:" + ex.Message);
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("scviewer.exe");
            Environment.Exit(0);
        }
    }
}
