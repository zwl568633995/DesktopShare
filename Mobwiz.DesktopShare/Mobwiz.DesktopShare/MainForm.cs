using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RDPCOMAPILib;

namespace Mobwiz.DesktopShare
{
    delegate void AddItemAction(string value);

    public partial class MainForm : Form
    {
        private RDPSession _rdpSession;
        private HostCaster _hostCaster;

        private bool _isRunning = false;

        private List<string> _attendees = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            this.SizeGripStyle = SizeGripStyle.Hide;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            btnStartCast.Enabled = true;
            btnStopCast.Enabled = false;

            _hostCaster = new HostCaster();
            _hostCaster.Start();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //this.Location = new Point( 
            //Screen.PrimaryScreen.Bounds.Width - 300);

            this.Left = Screen.PrimaryScreen.Bounds.Width - 500;
            this.Top = Screen.PrimaryScreen.Bounds.Height - 200;

        }

        private void btnStartCast_Click(object sender, EventArgs e)
        {
            //var dlg = new CaptureScreen()
            //{
            //    Owner = this
            //};
            //var result = dlg.ShowDialog();
            //if (result == DialogResult.No) return;

            //Rectangle rect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //if (result == DialogResult.Yes)
            //{
            //    rect = dlg.SelectedRectangle;
            //}

            ////默认第一个屏幕
            Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            Rectangle rect = screens[0].WorkingArea;
            try
            {
                btnStartCast.Enabled = false;

                _rdpSession = new RDPSession();

                _attendees.Clear();

                _rdpSession.OnAttendeeConnected += RdpSessionOnOnAttendeeConnected;
                _rdpSession.OnAttendeeDisconnected += RdpSessionOnOnAttendeeDisconnected;
                _rdpSession.OnControlLevelChangeRequest += RdpSessionOnOnControlLevelChangeRequest;

                //_rdpSession.SetDesktopSharedRect(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                _rdpSession.SetDesktopSharedRect(rect.X, rect.Y, rect.Right, rect.Bottom);

                _rdpSession.Open();


                IRDPSRAPIInvitation invitation = _rdpSession.Invitations.CreateInvitation("PalmaeTech", "CastGroup", "",
                    64);

                _hostCaster.StartCast(invitation.ConnectionString);

                btnStopCast.Enabled = true;
                notifyIcon1.Text = "屏幕广播已启动";
                notifyIcon1.ShowBalloonTip(1000, "屏幕广播启动成功", "屏幕广播已启动，程序已最小化到右下角系统通知区域。", ToolTipIcon.Info);

                this.ShowInTaskbar = false;
                this.Hide();

                _isRunning = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnStartCast.Enabled = true;
                btnStopCast.Enabled = false;
                _isRunning = false;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1000, "屏幕广播最小化", "屏幕广播最小化，请使用右下角系统通知区域菜单唤醒或退出程序", ToolTipIcon.Info);
            this.ShowInTaskbar = false;
            this.Hide();
            e.Cancel = true;
        }

        private void RdpSessionOnOnControlLevelChangeRequest(object pAttendee, CTRL_LEVEL requestedLevel)
        {
            //throw new NotImplementedException();
            // Do nothing...
        }

        private void RdpSessionOnOnAttendeeDisconnected(object pDisconnectInfo)
        {
            IRDPSRAPIAttendeeDisconnectInfo pDiscInfo = pDisconnectInfo as IRDPSRAPIAttendeeDisconnectInfo;
            Debug.WriteLine("Attendee Disconnected: " + pDiscInfo.Attendee.RemoteName + Environment.NewLine);

            _attendees.Remove(pDiscInfo.Attendee.RemoteName);
            lblMessage.Invoke(new Action(UpdateList));
        }

        private void RdpSessionOnOnAttendeeConnected(object pObjAttendee)
        {
            IRDPSRAPIAttendee pAttendee = pObjAttendee as IRDPSRAPIAttendee;
            pAttendee.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_VIEW;
            Debug.WriteLine("Attendee Connected: " + pAttendee.RemoteName + Environment.NewLine);

            _attendees.Add(pAttendee.RemoteName);

            lblMessage.Invoke(new Action(UpdateList));
        }

        private void btnStopCast_Click(object sender, EventArgs e)
        {
            try
            {
                StopCast();
            }
            catch (Exception ex)
            {
                btnStartCast.Enabled = true;
                btnStopCast.Enabled = false;
                _isRunning = false;
            }

            notifyIcon1.ShowBalloonTip(1000, "屏幕广播已停止", "屏幕广播已停止，重新开始，请点击开始广播，退出请使用右下角系统通知区域菜单", ToolTipIcon.Info);
            notifyIcon1.Text = "屏幕广播已停止";
        }

        private void StopCast()
        {
            if (_isRunning)
            {
                _attendees.Clear();
                UpdateList();
                _rdpSession?.Close();
                _hostCaster?.StopCast();
                btnStartCast.Enabled = true;
                btnStopCast.Enabled = false;
                _isRunning = false;
            }
        }

        private void UpdateList()
        {
            lblMessage.Text = $"已连接 {_attendees.Count} 客户端";
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopCast();
            }
            catch (Exception)
            {
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
