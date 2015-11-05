using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG04
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(1024, 1024);
            graphics = CreateGraphics();
        }

        private void lineDDA(int x1, int y1, int x2, int y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double len = Math.Abs(dx);
            if (Math.Abs(dy) > Math.Abs(dx))
            {
                len = Math.Abs(dy);
            }

            if ((x1 == x2) && dy < 0)
            {
                len = dy;
            }

            double xinc = dx / len;
            double yinc = dy / len; 
            if ((x1 == x2) && dy < 0)
            {
                yinc = Math.Abs(dy) / len;
            }

            double fx = x1;
            double fy = y1;
            double i = 1;

            if (x1 != x2)
            {
                while (i <= len)
                {
                    bitmap.SetPixel((int)Math.Round(fx), (int)Math.Round(fy), Color.FromArgb(255, 0, 0, 0));

                    fx = fx + xinc;
                    fy = fy + yinc;
                    i = i + 1;
                }
            }
            else if (x1 == x2)
            {
                i = y1;
                while (i != y2)
                {
                    bitmap.SetPixel((int)Math.Round(fx), (int)Math.Round(fy), Color.FromArgb(255, 0, 0, 0));

                    fx = fx + xinc;
                    fy = fy + yinc;
                    i = i + yinc;
                }
            }
        }

        private void lineBresenham(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;
            int error2;

            for (;;)
            {
                bitmap.SetPixel(x1, y1, Color.FromArgb(255, 0, 0, 0));

                if (x1 == x2 && y1 == y2)
                {
                    break;
                }

                error2 = err;
                if (error2 > -dx)
                {
                    err -= dy; x1 += sx;
                }

                if (error2 < dy)
                {
                    err += dx; y1 += sy;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Point[] P = new Point[]
            {
                new Point { X = 100, Y = 200 },
                new Point { X = 400, Y = 400 },

                new Point { X = 100, Y = 400 },
                new Point { X = 400, Y = 200 },

                new Point { X = 100, Y = 200 },
                new Point { X = 400, Y = 200 },

                new Point { X = 100, Y = 400 },
                new Point { X = 400, Y = 400 },

                new Point { X = 100, Y = 200 },
                new Point { X = 100, Y = 400 },

                new Point { X = 400, Y = 200 },
                new Point { X = 400, Y = 400 },

                new Point { X = 100, Y = 200 },
                new Point { X = 250, Y = 50 },

                new Point { X = 400, Y = 200 },
                new Point { X = 250, Y = 50 },
            };

            for (int i = 0; i < P.Length; i = i + 2)
            {
                lineDDA(P[i].X - 50, P[i].Y, P[i+1].X - 50, P[i+1].Y);
                lineBresenham(P[i].X + 300, P[i].Y, P[i + 1].X + 300, P[i + 1].Y);

                circleSinCos(P[i].X - 50, P[i].Y, i * 2 + 1);
                circleBresenham(P[i + 1].X + 300, P[i + 1].Y, i * 3 + 1);
            }

            graphics.DrawImage(bitmap, 0, 0);
        }

        private void circleSinCos(int x1, int y1, int radius)
        {
            int segments = radius * radius;
            int last_x = 0;
            int last_y = 0;

            for (int i = 0; i <= segments; i++)
            {
                double theta = 2.0f * 3.1415926f * i / segments;

                int x = (int)(x1 + radius * Math.Cos(theta));
                int y = (int)(y1 + radius * Math.Sin(theta));

                if (i != 0)
                {
                    lineBresenham(x, y, last_x, last_y);
                }

                last_x = x;
                last_y = y;

                bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void circleBresenham(int x1, int y1, int radius)
        {
            int x = radius;
            int y = 0;  
            int cd2 = 0;

            bitmap.SetPixel(x1 - radius, y1, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(x1 + radius, y1, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(x1, y1 - radius, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(x1, y1 + radius, Color.FromArgb(255, 0, 0, 0));
            while (x > y)
            { 
                cd2 -= (--x) - (++y);
                if (cd2 < 0)
                {
                    cd2 += x++;
                }
                bitmap.SetPixel(x1 - x, y1 - y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 - y, y1 - x, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 + y, y1 - x, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 + x, y1 - y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 - x, y1 + y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 - y, y1 + x, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 + y, y1 + x, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(x1 + x, y1 + y, Color.FromArgb(255, 0, 0, 0));
            }
        }
        
        /*
        private void CirclePoints(int x, int y)
        {
            bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(y, x, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(x, -y, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(y, -x, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(-x, y, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(-y, x, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(-x, -y, Color.FromArgb(255, 0, 0, 0));
            bitmap.SetPixel(-y, -x, Color.FromArgb(255, 0, 0, 0));
        }

        private void circleBresenham2(int x1, int y1, int radius)
        {
            int x = x1;
            int y = y1;
            int D = 1 - radius;

            CirclePoints(x, y);

            while (y > x)
            {
                if(D < 0)
                {
                    D = D + 2 * x + 3;
                }
                else
                {
                    D = D + 2 * (x - y) + 5;
                    y--;
                }

                x++;

                CirclePoints(x, y);
            }
        }
        */

        private void button3_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int x2 = Int32.Parse(textBox3.Text);
            int y2 = Int32.Parse(textBox4.Text);

            lineDDA(x1, y1, x2, y2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int x2 = Int32.Parse(textBox3.Text);
            int y2 = Int32.Parse(textBox4.Text);

            lineBresenham(x1, y1, x2, y2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int radius = Int32.Parse(textBox5.Text);

            circleSinCos(x1, y1, radius);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBox1.Text);
            int y1 = Int32.Parse(textBox2.Text);
            int radius = Int32.Parse(textBox5.Text);

            circleBresenham(x1, y1, radius);
        }
    }
}
