using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG07
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Bitmap bitmap2;
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap("C:\\Users\\admin\\Desktop\\grafika\\grayscale.jpg");
            bitmap2 = new Bitmap(bitmap.Width * 3, bitmap.Height * 3);
            graphics = CreateGraphics();
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

        private void fillImageStack(int x, int y)
        {
            if (bitmap.GetPixel(x, y) != Color.FromArgb(255, 0, 0, 0))
            {
                bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
                fillImageStack(x + 1, y);
                fillImageStack(x - 1, y);
                fillImageStack(x, y + 1);
                fillImageStack(x, y - 1);
            }
        }

        private void fillImageLine(int x, int y)
        {
            if (bitmap.GetPixel(x, y) != Color.FromArgb(255, 0, 0, 0))
            {
                int x_left = x-1;
                int x_right = x+1;

                bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));

                while (true)
                {
                    if (bitmap.GetPixel(x_left, y) != Color.FromArgb(255, 0, 0, 0))
                    {
                        bitmap.SetPixel(x_left, y, Color.FromArgb(255, 0, 0, 0));
                        x_left--;
                    }
                    else if (bitmap.GetPixel(x_right, y) != Color.FromArgb(255, 0, 0, 0))
                    {
                        bitmap.SetPixel(x_right, y, Color.FromArgb(255, 0, 0, 0));
                        x_right++;
                    }
                    else
                    {
                        break;
                    }
                }

                fillImageLine(x, y + 1);
                fillImageLine(x, y - 1);
            }
        }

        private int[,] pattern_matrix(int type)
        {
            int[,] matrix;
            switch(type) {
                case 0:
                    matrix = new int[3, 3] {
                        { 0, 0, 0},
                        { 0, 0, 0},
                        { 0, 0, 0},
                    };
                    break;
                case 1:
                    matrix = new int[3, 3] {
                        { 0, 0, 0},
                        { 0, 1, 0},
                        { 0, 0, 0},
                    };
                    break;
                case 2:
                    matrix = new int[3, 3] {
                        { 0, 0, 0},
                        { 1, 1, 0},
                        { 0, 0, 0},
                    };
                    break;
                case 3:
                    matrix = new int[3, 3] {
                        { 0, 0, 0},
                        { 1, 1, 0},
                        { 0, 1, 0},
                    };
                    break;
                case 4:
                    matrix = new int[3, 3] {
                        { 0, 0, 0},
                        { 1, 1, 1},
                        { 0, 1, 0},
                    };
                    break;
                case 5:
                    matrix = new int[3, 3] {
                        { 0, 0, 1},
                        { 1, 1, 1},
                        { 0, 1, 0},
                    };
                    break;
                case 6:
                    matrix = new int[3, 3] {
                        { 0, 0, 1},
                        { 1, 1, 1},
                        { 1, 1, 0},
                    };
                    break;
                case 7:
                    matrix = new int[3, 3] {
                        { 1, 0, 1},
                        { 1, 1, 1},
                        { 1, 1, 0},
                    };
                    break;
                case 8:
                    matrix = new int[3, 3] {
                        { 1, 0, 1},
                        { 1, 1, 1},
                        { 1, 1, 1},
                    };
                    break;
                case 9:
                    matrix = new int[3, 3] {
                        { 1, 1, 1},
                        { 1, 1, 1},
                        { 1, 1, 1},
                    };
                    break;
                default:
                    matrix = new int[3, 3] {
                        { 0, 0, 0},
                        { 0, 0, 0},
                        { 0, 0, 0},
                    };
                    break;
            }

            return matrix;
        }

        private void halftone()
        {
            Random generator = new Random();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int randomNum = generator.Next(0, 255);

                    Color c = bitmap.GetPixel(x, y);
                    if (c.R < 50)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
                    }
                    else if (c.R > 200)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                    }
                    else
                    {
                        if (randomNum < c.R)
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
                        }
                        else
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                        }
                    }
                }
            }
        }

        private void dither()
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int I = color.R;

                    int matrix_type = (int)((1 - (I / 255.0)) * 9);

                    for (int x2 = 0; x2 < 3; x2++)
                    {
                        for (int y2 = 0; y2 < 3; y2++)
                        {
                            int[,] matrix = pattern_matrix(matrix_type);

                            if (matrix[x2, y2] == 1)
                            {
                                bitmap2.SetPixel(x * 3 + x2, y * 3 + y2, Color.FromArgb(255, 0, 0, 0));
                            }
                            else
                            {
                                bitmap2.SetPixel(x * 3 + x2, y * 3 + y2, Color.FromArgb(255, 255, 255, 255));
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //circleBresenham(100, 100, 100);
            //fillImageStack(50, 50);
            //fillImageLine(50, 50);
            //bitmap.SetPixel(50, 50, Color.FromArgb(255,255,0,0));

            halftone();
            dither();

            graphics.DrawImage(bitmap, 0, 0);
            //graphics.DrawImage(bitmap2, 0, 0);
        }
    }
}
