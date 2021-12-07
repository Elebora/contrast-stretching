using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contrast_stretching
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

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap orijinal = new Bitmap(pictureBox1.Image);

            byte[] rKanaliRenkDizisi = new byte[256];
            byte[] gKanaliRenkDizisi = new byte[256];
            byte[] bKanaliRenkDizisi = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                rKanaliRenkDizisi[i] = gKanaliRenkDizisi[i] = bKanaliRenkDizisi[i] = 0;
            }

            for (int x = 0; x < orijinal.Width; x++)
            {
                for (int y = 0; y < orijinal.Height; y++)
                {
                    rKanaliRenkDizisi[orijinal.GetPixel(x, y).R]++;
                    gKanaliRenkDizisi[orijinal.GetPixel(x, y).G]++;
                    bKanaliRenkDizisi[orijinal.GetPixel(x, y).B]++;
                }
            }

            int rMax, gMax, bMax;
            rMax = gMax = bMax = 0;

            for (int i = 255; i >= 0; i--)
            {
                if (rKanaliRenkDizisi[i] != 0)
                {
                    rMax = i; break;
                }
            }
            for (int i = 255; i >= 0; i--)
            {
                if (gKanaliRenkDizisi[i] != 0)
                {
                    gMax = i; break;
                }
            }
            for (int i = 255; i >= 0; i--)
            {
                if (bKanaliRenkDizisi[i] != 0)
                {
                    bMax = i; break;
                }
            }
            int rMin, gMin, bMin;
            rMin = gMin = bMin = 0;

            for (int i = 0; i < 256; i++)
            {
                if (rKanaliRenkDizisi[i] != 0)
                {
                    rMin = i; break;
                }
            }
            for (int i = 0; i < 256; i++)
            {
                if (gKanaliRenkDizisi[i] != 0)
                {
                    gMin = i; break;
                }
            }
            for (int i = 0; i < 256; i++)
            {
                if (bKanaliRenkDizisi[i] != 0)
                {
                    bMin = i; break;
                }
            }

            int gmin = 0; int gmax = 255;
            double rCikti, gCikti, bCikti;
            rCikti = gCikti = bCikti = 0;

            double rCarpan = 0; double gCarpan = 0; double bCarpan = 0;
            Bitmap cikti = new Bitmap(orijinal.Width, orijinal.Height);

            for (int x = 0; x < orijinal.Width; x++)
            {
                for (int y = 0; y < orijinal.Height; y++)
                {
                    rCarpan = ((double)(gmax - gmin) / (double)(rMax - rMin));
                    rCikti = (double)((orijinal.GetPixel(x, y).R - rMin) * (rCarpan) + gmin);
                    gCarpan = (double)(gmax - gmin) / (double)(gMax - gMin);
                    gCikti = (double)((orijinal.GetPixel(x, y).G - gMin) * (gCarpan) + gmin);
                    bCarpan = (double)(gmax - gmin) / (double)(bMax - bMin);
                    bCikti = (double)((orijinal.GetPixel(x, y).B - bMin) * (bCarpan) + gmin);
                    cikti.SetPixel(x, y, Color.FromArgb((int)rCikti, (int)bCikti, (int)bCikti));
                }
            }
            pictureBox2.Image = cikti;
        }
    
    }
}
