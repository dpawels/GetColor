using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GetColor
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll")]
        public static extern int GetPixel(
                System.IntPtr hdc,
                int nXPos,
                int nYPos);
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr wnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr wnd, IntPtr dc);

        private Font font = new Font("Arial", 10);
        private int cntPixel = 8;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100;
            timer1.Enabled = true;
            this.TopMost = true;
            this.Text = "GetColor " + cntPixel.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float textHeight = 18.0f;
            int boxHeight = 90;
            //int cntPixel = 3;

            Point pt = Control.MousePosition;
            IntPtr dc = GetDC(IntPtr.Zero);
            Graphics g = this.CreateGraphics();
            Color col;
            //int tileWidth = this.Width / (2 * cntPixel + 1);
            if (cntPixel < 1) cntPixel = 1;
            int tileWidth = this.Width / (cntPixel);
            //int tileHeight = (this.Height - boxHeight) / (2 * cntPixel + 1);
            int tileHeight = (this.Height - boxHeight) / (cntPixel);
            int tileX, tileY;
            int n = 0;
            int sumR = 0, sumG = 0, sumB = 0;
            int sumH = 0, sumS = 0, sumI = 0;
            if (pt.X > 1500)
                tileWidth = tileWidth;
            //for (int x = pt.X - cntPixel; x <= pt.X + cntPixel; x++)
            for (int x = pt.X; x < pt.X + cntPixel; x++)
                {
                for (int y = pt.Y; y < pt.Y + cntPixel; y++)
                {
                    col = ColorTranslator.FromWin32(GetPixel(dc, x, y));
                    sumR += col.R;
                    sumG += col.G;
                    sumB += col.B;
                    sumH += (int)(col.GetHue());
                    sumS += (int)(255.0f * col.GetSaturation());
                    sumI += (int)(255.0f * col.GetBrightness());
                    tileX = (x - pt.X + cntPixel) * tileWidth;
                    tileY = (y - pt.Y + cntPixel) * tileHeight;
                    g.FillRectangle(new SolidBrush(col), new Rectangle(tileX, tileY, tileWidth, tileHeight));
                    n++;
                }
            }
            // zuletzt mittleres Pixel merken
            col = ColorTranslator.FromWin32(GetPixel(dc, pt.X, pt.Y));
            // Mittelwerte bilden
            int avgR = sumR / n;
            int avgG = sumG / n;
            int avgB = sumB / n;
            int avgH = sumH / n;
            int avgS = sumS / n;
            int avgI = sumI / n;
           // g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, this.Height - boxHeight, this.Width, boxHeight));
            g.DrawString("X " + Control.MousePosition.X.ToString(), font, Brushes.White, 10, this.ClientSize.Height - 3 * textHeight);
            g.DrawString(" Y " + Control.MousePosition.Y.ToString(), font, Brushes.White, 50, this.ClientSize.Height - 3 * textHeight);
            if (col.IsNamedColor)
                g.DrawString(col.Name.ToString(), font, Brushes.White, 140, this.ClientSize.Height - 3*textHeight);
            //g.DrawString("avg", font, Brushes.White, 5, this.ClientSize.Height - 2 * textHeight);
            g.DrawString("R " + avgR.ToString(), font, Brushes.Red, 10, this.ClientSize.Height - 2 * textHeight);
            g.DrawString("G " + avgG.ToString(), font, Brushes.LightGreen, 50, this.ClientSize.Height - 2 * textHeight);
            g.DrawString("B " + avgB.ToString(), font, Brushes.LightBlue, 90, this.ClientSize.Height - 2 * textHeight);
            g.DrawString("R " + col.R.ToString(), font, Brushes.Red, 140, this.ClientSize.Height - 2 * textHeight);
            g.DrawString("G " + col.G.ToString(), font, Brushes.LightGreen, 180, this.ClientSize.Height - 2 * textHeight);
            g.DrawString("B " + col.B.ToString(), font, Brushes.LightBlue, 220, this.ClientSize.Height - 2 * textHeight);
            int nHue = (int)(col.GetHue());
            int nSat = (int)(255.0f * col.GetSaturation());
            int nInt = (int)(255.0f * col.GetBrightness());
            g.DrawString("H " + avgH.ToString(), font, Brushes.Magenta, 10, this.ClientSize.Height - textHeight);
            g.DrawString("S " + avgS.ToString(), font, Brushes.Yellow, 50, this.ClientSize.Height - textHeight);
            g.DrawString("I " + avgI.ToString(), font, Brushes.LightGray, 90, this.ClientSize.Height - textHeight);
            g.DrawString("H " + nHue.ToString(), font, Brushes.Magenta, 140, this.ClientSize.Height - textHeight);
            g.DrawString("S " + nSat.ToString(), font, Brushes.Yellow, 180, this.ClientSize.Height - textHeight);
            g.DrawString("I " + nInt.ToString(), font, Brushes.LightGray, 220, this.ClientSize.Height - textHeight);

            ReleaseDC(IntPtr.Zero, dc);
            g.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys k = e.KeyData;
            int result = 0;
            switch (k)
            {
                case Keys.D0: { result = 0; break; }
                case Keys.D1: { result = 1; break; }
                case Keys.D2: { result = 2; break; }
                case Keys.D3: { result = 3; break; }
                case Keys.D4: { result = 4; break; }
                case Keys.D5: { result = 5; break; }
                case Keys.D6: { result = 6; break; }
                case Keys.D7: { result = 7; break; }
                case Keys.D8: { result = 8; break; }
                case Keys.D9: { result = 9; break; }

                case Keys.F5: { result = 8; break; }
                case Keys.F6: { result = 16; break; }
                case Keys.F7: { result = 32; break; }
                case Keys.F8: { result = 64; break; }
                case Keys.F9: { result = 128; break; }
                case Keys.F10: { result = 256; break; }

                case Keys.Multiply: { result = cntPixel * 2; break; }
                case Keys.Divide: { result = cntPixel / 2; break; }
            }
            cntPixel = result;
            this.Text = "GetColor " + cntPixel.ToString();
        }
    }
}