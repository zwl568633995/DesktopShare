namespace Mobwiz.DesktopShare
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStartCast = new System.Windows.Forms.Button();
            this.btnStopCast = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartCast
            // 
            this.btnStartCast.Location = new System.Drawing.Point(13, 13);
            this.btnStartCast.Name = "btnStartCast";
            this.btnStartCast.Size = new System.Drawing.Size(92, 37);
            this.btnStartCast.TabIndex = 0;
            this.btnStartCast.Text = "开始广播";
            this.btnStartCast.UseVisualStyleBackColor = true;
            this.btnStartCast.Click += new System.EventHandler(this.btnStartCast_Click);
            // 
            // btnStopCast
            // 
            this.btnStopCast.Location = new System.Drawing.Point(127, 13);
            this.btnStopCast.Name = "btnStopCast";
            this.btnStopCast.Size = new System.Drawing.Size(92, 37);
            this.btnStopCast.TabIndex = 1;
            this.btnStopCast.Text = "停止广播";
            this.btnStopCast.UseVisualStyleBackColor = true;
            this.btnStopCast.Click += new System.EventHandler(this.btnStopCast_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(234, 25);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(71, 12);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "已连接 0 人";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "屏幕广播";
            this.notifyIcon1.BalloonTipTitle = "屏幕广播";
            this.notifyIcon1.ContextMenuStrip = this.notifyMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "屏幕广播已停止";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // notifyMenu
            // 
            this.notifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.notifyMenu.Name = "notifyMenu";
            this.notifyMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "退出";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 63);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnStopCast);
            this.Controls.Add(this.btnStartCast);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "桌面广播工具";
            this.TopMost = true;
            this.notifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartCast;
        private System.Windows.Forms.Button btnStopCast;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
    }
}

