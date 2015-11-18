using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG06
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

        private float Bernstein(int i, float t)
        {
            switch (i)
            {
                case 0: return -(t * t * t) + 3 * (t * t) - 3 * t + 1;
                case 1: return 3 * (t * t * t) - 6 * (t * t) + 3 * t;
                case 2: return -3 * (t * t * t) + 3 * (t * t);
                case 3: return t * t * t;
            }

            return 0;
        }

        private void lineBresenham(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;
            int error2;

            while (true)
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

        private const int step = 20;

        private void bezier_surface()
        {
            PointF[,] points = new PointF[step, step];
            PointF[,] BP = new PointF[4, 4] {
                { new Point(100, 100), new Point(150, 30), new Point(230, 100), new Point(320, 100)},
                { new Point(50, 170), new Point(250, 280), new Point(240, 190), new Point(440, 250)},
                { new Point(30, 220), new Point(110, 210), new Point(200, 300), new Point(510, 300)},
                { new Point(20, 360), new Point(130, 300), new Point(230, 240), new Point(300, 320)}
            };

            for (int x = 0; x < step; x++)
            {
                for (int y = 0; y < step; y++)
                {
                    float u = x / (float)step;
                    float v = y / (float)step;

                    PointF p = new PointF(0, 0);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            p.X += BP[i, j].X * Bernstein(i, u) * Bernstein(j, v);
                            p.Y += BP[i, j].Y * Bernstein(i, u) * Bernstein(j, v);
                        }
                    }

                    points[x, y] = p;
                }
            }

            for (int x = 0; x < step; x++)
            {
                for (int y = 0; y < step; y++)
                {
                    if (x == 0)
                        graphics.DrawLine(Pens.Green, points[x, y], points[x + 1, y]);
                    else
                        graphics.DrawLine(Pens.Green, points[x, y], points[x - 1, y]);

                    if(y == 0)
                        graphics.DrawLine(Pens.Blue, points[x, y], points[x, y + 1]);
                    else
                        graphics.DrawLine(Pens.Blue, points[x, y], points[x, y - 1]);
                }
            }

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    graphics.DrawEllipse(Pens.Red, BP[x, y].X - 5, BP[x, y].Y - 5, 10, 10);
                }
            }

            graphics.DrawImage(bitmap, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bezier_surface();
        }
    }
}
