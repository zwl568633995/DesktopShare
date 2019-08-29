using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mobwiz.DesktopShare
{
    public partial class CaptureScreen : Form
    {
        private int x;
        private int y;
        private int x1;
        private int y1;
        private bool isMouseDown = false;

        public CaptureScreen()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

            this.BackColor = Color.White;
            this.Opacity = 0.4;
            this.TransparencyKey = Color.CornflowerBlue;

            this.MouseDown += OnMouseDown;
            this.MouseMove += OnMouseMove;
            this.MouseUp += OnMouseUp;

            this.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        public Rectangle SelectedRectangle { get; set; }

        private void OnMouseUp(object sender, MouseEventArgs args)
        {
            if (isMouseDown && args.Button == MouseButtons.Left)
            {
                SelectedRectangle = new Rectangle(x, y, Math.Abs(args.X - x), Math.Abs(args.Y - y));

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (isMouseDown)
            {
                var width = args.X - x;
                var height = args.Y - y;
                var g = CreateGraphics();
                g.Clear(BackColor);
                g.FillRectangle(Brushes.CornflowerBlue, x < MousePosition.X ? x : MousePosition.X, y < MousePosition.Y ? y : MousePosition.Y, width + 1, height + 1);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                x = MousePosition.X;
                y = MousePosition.Y;
            }
        }


    }
}
