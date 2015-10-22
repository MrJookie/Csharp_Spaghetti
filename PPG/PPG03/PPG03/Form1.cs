using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PPG03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap("C:\\Users\\admin\\Desktop\\grafika\\cb.png");
            Graphics g = this.CreateGraphics();

            //g.DrawImage(b, 0, 40);

            quadtree(b, 0, 0, b.Width, b.Height);
            //rle(b);

            //string[] lines = { Convert.ToString(b.Width), Convert.ToString(b.Height) };
            //System.IO.File.WriteAllLines(@"C:\\Users\\admin\\Desktop\\grafika\\quadtree.txt", lines);
        }

        private void rle(Bitmap b)
        {
            Color c1 = b.GetPixel(0, 0);

            for (int y = 0; y < b.Height; y++)
            {
                int colorCounter = 0;
                int colorCounter2 = 0;

                for (int x = 0; x < b.Width; x++)
                {
                    Color c2 = b.GetPixel(x, y);
                    if (c1 == c2)
                    {
                        colorCounter++;
                    }
                    else
                    {
                        colorCounter2++;
                    }
                }

                Console.WriteLine("("+colorCounter+",C1), ("+colorCounter2+",C2)");
            }
        }

        //to fix
        private string quadtree(Bitmap b, int Xmin, int Ymin, int Xmax, int Ymax)
        {
            Color color1 = b.GetPixel(Xmin, Ymin);

            bool differentColor = true;

            for (int x = Xmin; x < Xmax; x++)
            {
                for (int y = Ymin; y < Ymax; y++)
                {
                    Color color2 = b.GetPixel(x, y);
                    if (color2 != color1)
                    {
                        differentColor = true;
                        break;
                    }

                    if (differentColor == false)
                    {
                        return "different color";
                        //return Convert.ToString(soucasnaBarva);
                    }

                    string output = "(";
                    output += quadtree(b, Xmin + Xmax / 2, Ymin, Xmax / 2, Ymax / 2);
                    output += quadtree(b, Xmin, Ymin, Xmax / 2, Ymax / 2);
                    output += quadtree(b, Xmin, Ymin + Ymax / 2, Xmax / 2, Ymax / 2);
                    output += quadtree(b, Xmin + Xmax / 2, Ymin + Ymax / 2, Xmax / 2, Ymax / 2);
                    output += ")";

                    Console.WriteLine(output);

                    return output;
                }
            }

            return "*"; //?
        }
    }
}
