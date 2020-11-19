using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// ReSharper disable PossibleLossOfFraction

namespace lab_four_graphics
{
    public partial class Form1 : Form
    {
        private readonly int[] _temperatures =
        {
            5, 11, 12, 18, 2, -6, -8, 5, 8, 16, 21, 17, 0, -1, 4, -6,
            3, 3, 3, 0, -2, -8, 0, 17, 7, 9, 16, 10, 16, 19, 12
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void drawGraphic_Click(object sender, EventArgs e)
        {
            var g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            var max = _temperatures.Concat(new[] {-1}).Max();
            var size = pictureBox1.Size;
            var dy = size.Height/(2*max);
            var dx = size.Width/31;
            var p = new Pen(Color.Navy);
            var fn = new Font("Segoe UI", 10, FontStyle.Bold, GraphicsUnit.Point);
            var offset = g.MeasureString(Convert.ToString(max), fn);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };
            
            g.DrawLine(p, offset.Width, size.Height/2, size.Width, size.Height/2);
            g.DrawLine(p, offset.Width, 0, offset.Width, size.Height);
            //текст на оси y
            for (int i = 0, y = size.Height/2; i <= max; i++) {
                g.DrawString(Convert.ToString(i), fn, Brushes.Tomato,10, y-dy*i, sf);
                g.DrawLine(p, offset.Width, y-dy*i, size.Width, y-dy*i);
                
                g.DrawString(Convert.ToString(i), fn, Brushes.Tomato,10, y+dy*i, sf);
                g.DrawLine(p, offset.Width, y+dy*i, size.Width, y+dy*i);
            }
            // текст на оси x
            for (int i = 1, x = dx; i <= _temperatures.Length; i++, x += dx) {
                g.DrawString(Convert.ToString(i), fn, Brushes.Tomato, x,(size.Height-offset.Height-2)/2, sf);
                g.DrawLine(p, x, size.Height / 2 - 5 , x,
                size.Height/2+5);
            }
            p.Dispose();
            p = new Pen(Color.Teal, 3) {DashStyle = System.Drawing.Drawing2D.DashStyle.Dot};
            g.DrawEllipse(Pens.Tomato, dx-3, pictureBox1.Size.Height/2-_temperatures[0]*dy-3, 6, 6);
            for (int i = 1, x = dx; i <= _temperatures.Length-1; i++, x+=dx) {
                g.DrawLine(p, x, pictureBox1.Size.Height/2-_temperatures[i-1]*dy, x+dx, pictureBox1.Size.Height/2-_temperatures[i]*dy);
                g.DrawEllipse(Pens.Tomato, x+dx-3,pictureBox1.Size.Height/2-_temperatures[i]*dy-3, 6, 6);
            }
            p.Dispose();
        }
    }
}