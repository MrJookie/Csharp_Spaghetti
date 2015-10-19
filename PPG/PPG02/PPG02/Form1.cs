using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG02
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        int colorR = 0;
        int colorG = 0;
        int colorB = 0;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(256, 256);
        }

        private void draw(int r, int g, int b)
        {
            Graphics graphics = CreateGraphics();

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {

                    bitmap.SetPixel(i, j, Color.FromArgb(r, i, j));
                }
            }

            graphics.DrawImage(bitmap, 0, 0);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Point point;
            point = e.Location;

            if (point.X < 256 && point.X >= 0 && point.Y < 256 && point.Y >= 0)
            {
                int colorG, colorB;

                colorG = point.X;
                colorB = point.Y;

                numericUpDown2.Value = colorG;
                numericUpDown3.Value = colorB;

                panel1.BackColor = Color.FromArgb(colorR, colorG, colorB);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw(colorR, colorG, colorB);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            colorR = hScrollBar1.Value;
            draw(colorR, colorG, colorB);
            numericUpDown1.Value = colorR;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            colorR = (int)numericUpDown1.Value;
            panel1.BackColor = Color.FromArgb(colorR, colorG, colorB);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            colorG = (int)numericUpDown2.Value;
            panel1.BackColor = Color.FromArgb(colorR, colorG, colorB);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            colorB = (int)numericUpDown3.Value;
            panel1.BackColor = Color.FromArgb(colorR, colorG, colorB);
        }
    }
}
