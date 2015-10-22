using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private Bitmap BitmapToGrayscale(Bitmap b, int m)
        {
            Bitmap b1 = new Bitmap(b.Width, b.Height);

            for (int x = 0; x < b1.Width; x++)
            {
                for (int y = 0; y < b1.Height; y++)
                {
                    Color c = b.GetPixel(x, y);

                    int intensity = 0;
                    if (m == 1)
                    {
                        //averaged - incorrect
                        intensity = (c.R + c.G + c.B) / 3;
                    }
                    else
                    {
                        //correct
                        intensity = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                    }

                    b1.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }

            return b1;
        }

        private Bitmap SamplePixels(Bitmap b, int k)
        {
            Bitmap b1 = new Bitmap(b.Width / k, b.Height / k);

            for (int x = 0; x < b1.Width; x++)
            {
                for (int y = 0; y < b1.Height; y++)
                {
                    Color c = b.GetPixel(x * k, y * k);

                    b1.SetPixel(x, y, c);
                }
            }

            return b1;
        }

        private Bitmap SuperSamplePixels(Bitmap b, int k)
        {
            Bitmap b1 = new Bitmap(b.Width / k, b.Height / k);

            for (int x = 0; x < b1.Width; x++)
            {
                for (int y = 0; y < b1.Height; y++)
                {
                    int colorR = 0;
                    int colorG = 0;
                    int colorB = 0;
                    int diff = 0;

                    for (int x1 = 0; x1 < k; x1++)
                    {
                        for (int y1 = 0; y1 < k; y1++)
                        {
                            Color c = b.GetPixel(x * k + x1, y * k + y1);

                            colorR += c.R;
                            colorG += c.G;
                            colorB += c.B;

                            diff++;
                        }
                    }


                    b1.SetPixel(x, y, Color.FromArgb(colorR / diff, colorG / diff, colorB / diff));
                }
            }

            return b1;
        }

        private Bitmap Quantization(Bitmap b, int k)
        {
            Bitmap b1 = new Bitmap(b.Width, b.Height);

            int[] quantizationTable = new int[k];
            int interval = 256 / k;
            for (int i = 0; i < k; i++)
            {
                quantizationTable[i] = interval * i;
            }

            for (int x = 0; x < b1.Width; x++)
            {
                for (int y = 0; y < b1.Height; y++)
                {
                    Color c = b.GetPixel(x, y);

                    int intensityR = 0;

                    for (int i = 0; i < quantizationTable.Length; i++)
                    {
                        if (c.R > quantizationTable[i])
                        {
                            if (quantizationTable[i] == 0)
                            {
                                intensityR = quantizationTable[i];
                            }
                            else
                            {
                                if (quantizationTable[i] + interval > 255)
                                {
                                    intensityR = 255;
                                }
                                else
                                {
                                    intensityR = quantizationTable[i] + interval;
                                }
                            }
                        }
                    }

                    b1.SetPixel(x, y, Color.FromArgb(intensityR, intensityR, intensityR));
                }
            }

            return b1;
        }

        int clicks = 2;
        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap("C:\\Users\\admin\\Desktop\\grafika\\grayscale.jpg");
            Graphics g = this.CreateGraphics();

            //g.DrawImage(b, 0, 40);
            //g.DrawImage(BitmapToGrayscale(b, 1), 0, 40);
            //g.DrawImage(BitmapToGrayscale(b, 2), 0, 40);

            //g.DrawImage(SamplePixels(b, clicks++), 0, 40);
            //g.DrawImage(SuperSamplePixels(b, clicks++), 0, 40);

            g.DrawImage(Quantization(b, clicks++), 0, 40);
        }
    }
}
