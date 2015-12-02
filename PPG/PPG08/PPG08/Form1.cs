using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG08
{
    public class Point3
    {
        public Point3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X = 0;
        public double Y = 0;
        public double Z = 0;
    }

    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Graphics graphics2;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(1024, 1024);
            graphics = CreateGraphics();
            graphics2 = Graphics.FromImage(bitmap);
        }

        private void lineBresenham(int x1, int y1, int x2, int y2)
        {
            x1 += 200;
            y1 += 200;
            x2 += 200;
            y2 += 200;

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

        double alfa = 0;

        private void rotateVert(Point3[] verts, int vertNum, double alfa)
        {
            double x = 0;
            double y = 0;
            double z = 0;

            x = verts[vertNum].X * Math.Cos(alfa) - verts[vertNum].Y * Math.Sin(alfa);
            y = verts[vertNum].X * Math.Sin(alfa) + verts[vertNum].Y * Math.Cos(alfa);

            verts[vertNum].X = x;
            verts[vertNum].Y = y;

            z = verts[vertNum].Z * Math.Cos(alfa) - verts[vertNum].Y * Math.Sin(alfa);
            y = verts[vertNum].Z * Math.Sin(alfa) + verts[vertNum].Y * Math.Cos(alfa);

            verts[vertNum].Z = z;
            verts[vertNum].Y = y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point3[] verts = { 
                                 new Point3(-100, 100, 0),
                                 new Point3(100, 100, 0),
                                 new Point3(100, -100, 0),
                                 new Point3(-100, -100, 0),
                                 new Point3(0, 0, 100)};

            graphics2.Clear(Color.White);

            rotateVert(verts, 0, alfa);
            rotateVert(verts, 1, alfa);
            rotateVert(verts, 2, alfa);
            rotateVert(verts, 3, alfa);
            rotateVert(verts, 4, alfa);

            lineBresenham((int)verts[0].X, (int)verts[0].Y, (int)verts[1].X, (int)verts[1].Y);
            lineBresenham((int)verts[1].X, (int)verts[1].Y, (int)verts[2].X, (int)verts[2].Y);
            lineBresenham((int)verts[2].X, (int)verts[2].Y, (int)verts[3].X, (int)verts[3].Y);
            lineBresenham((int)verts[3].X, (int)verts[3].Y, (int)verts[0].X, (int)verts[0].Y);

            lineBresenham((int)verts[0].X, (int)verts[0].Y, (int)verts[4].X, (int)verts[4].Y);
            lineBresenham((int)verts[1].X, (int)verts[1].Y, (int)verts[4].X, (int)verts[4].Y);
            lineBresenham((int)verts[2].X, (int)verts[2].Y, (int)verts[4].X, (int)verts[4].Y);
            lineBresenham((int)verts[3].X, (int)verts[3].Y, (int)verts[4].X, (int)verts[4].Y);


            graphics.DrawImage(bitmap, 200, 0);

            alfa += 0.1;
        }
    }
}
