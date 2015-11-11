using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG05
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private List<Point> points = new List<Point>();
        private List<Point> tangents = new List<Point>();

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(1024, 1024);
            graphics = CreateGraphics();
        }

        private void lineBresenham(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;
            int error2;

            while(true)
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

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point point;
            point = e.Location;

            Console.WriteLine(point.X + " " + point.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bitmap.SetPixel(point.X - 1, point.Y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(point.X + 1, point.Y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(point.X, point.Y - 1, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 0, 0, 0));
                bitmap.SetPixel(point.X, point.Y + 1, Color.FromArgb(255, 0, 0, 0));

                points.Add(point);
            }
            else if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                bitmap.SetPixel(point.X - 1, point.Y, Color.FromArgb(255, 255, 0, 0));
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 255, 0, 0));
                bitmap.SetPixel(point.X + 1, point.Y, Color.FromArgb(255, 255, 0, 0));
                bitmap.SetPixel(point.X, point.Y - 1, Color.FromArgb(255, 255, 0, 0));
                bitmap.SetPixel(point.X, point.Y, Color.FromArgb(255, 255, 0, 0));
                bitmap.SetPixel(point.X, point.Y + 1, Color.FromArgb(255, 255, 0, 0));

                tangents.Add(point);
            }

            graphics.DrawImage(bitmap, 0, 0);
        }

        private double F1(double t)
        {
            return 2 * (t * t * t) - 3 * (t * t) + 1;
        }

        private double F2(double t)
        {
            return -2 * (t * t * t) + 3 * (t * t);
        }

        private double F3(double t)
        {
            return t * t * t - 2 * (t * t) + t;
        }

        private double F4(double t)
        {
            return t * t * t - t * t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Point> toDraw = new List<Point>();

            for (double t = 0; t < 1; t = t + 0.001)
            {
                double x = points[0].X * F1(t) + points[1].X * F2(t) + tangents[0].X * F3(t) + tangents[1].X * F4(t);
                double y = points[0].Y * F1(t) + points[1].Y * F2(t) + tangents[0].Y * F3(t) + tangents[1].Y * F4(t);

                Point p = new Point((int)x, (int)y);

                toDraw.Add(p);
            }

            for (int i = 0; i < toDraw.Count(); i = i + 2)
            {
                lineBresenham(toDraw[i].X, toDraw[i].Y, toDraw[i + 1].X, toDraw[i + 1].Y);
            }

            graphics.DrawImage(bitmap, 0, 0);
        }

        private double B1(double t)
        {
            return -(t * t * t) + 3 * (t * t) - 3 * t + 1;
        }

        private double B2(double t)
        {
            return 3 * (t * t * t) - 6 * (t * t) + 3 * t;
        }

        private double B3(double t)
        {
            return -3 * (t * t * t) + 3 * (t * t);
        }

        private double B4(double t)
        {
            return t * t * t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Point> toDraw = new List<Point>();

            for (double t = 0; t < 1; t = t + 0.001)
            {
                double x = points[0].X * B1(t) + points[1].X * B2(t) + points[2].X * B3(t) + points[3].X * B4(t);
                double y = points[0].Y * B1(t) + points[1].Y * B2(t) + points[2].Y * B3(t) + points[3].Y * B4(t);

                Point p = new Point((int)x, (int)y);

                toDraw.Add(p);
            }

            for (int i = 0; i < toDraw.Count(); i = i + 2)
            {
                lineBresenham(toDraw[i].X, toDraw[i].Y, toDraw[i + 1].X, toDraw[i + 1].Y);
            }

            graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
