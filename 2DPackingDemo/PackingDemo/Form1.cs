using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingDemo
{
    public partial class Form1 : Form
    {
        private readonly BoxContainer boxContainer = new BoxContainer(1920, 1080);

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
        }

        private static readonly Random random = new Random();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Stopwatch stopwatch = new Stopwatch();
            if (e.Button == MouseButtons.Left)
            {
                int width = random.Next(ClientSize.Width / 16, ClientSize.Width / 4 + 1);
                int height = random.Next(ClientSize.Height / 16, ClientSize.Height / 4 + 1);
                boxContainer.Add(new Box(width, height));
                stopwatch.Restart();
                var noPackedBoxes = boxContainer.Packing();
                Text = stopwatch.ElapsedTicks.ToString();
                if (noPackedBoxes.Any())
                {
                    MessageBox.Show("box full");
                }
                Invalidate();
            }
            else
            {
                boxContainer.Clear();
                boxContainer.Packing();
            }
            Invalidate();
        }

        static readonly Font font = new Font(new FontFamily("宋体"), 70);
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            base.OnPaint(e);
            boxContainer?.DrawBox(e.Graphics);
            e.Graphics.DrawString(Text, font, Brushes.Red, new PointF(0, 0));
        }
    }
}
