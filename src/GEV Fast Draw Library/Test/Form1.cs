using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using GEV.FDL;
using Cognex.VisionPro;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Task[] tasks = new Task[1000];
            Stopwatch sw = Stopwatch.StartNew();
            //FDLImage img = null;
            //for (int i = 0; i < 1000; i++)
            //{
            //    tasks[i] = new Task(() =>
            //    {
            //        FDLImage timg = new FDLImage(320, 240);
            //        timg.Clear(System.Drawing.Color.Red);
            //        timg.DrawLine(0, 0, 100, 100, System.Drawing.Color.AliceBlue);
            //        timg.DrawEllipse(50, 50, 20, 100, System.Drawing.Color.Yellow);
            //    });
            //    tasks[i].Start();
            //}
            //Task.WaitAll(tasks);
            //MessageBox.Show(String.Format("{0} ms", sw.ElapsedMilliseconds));


            //sw.Restart();
            //for (int i = 0; i < 1000; i++)
            //{
            //    tasks[i] = new Task(() =>
            //    {

            //        WriteableBitmap wimg = new WriteableBitmap(320, 240, 96, 96, PixelFormats.Pbgra32, BitmapPalettes.Gray256);

            //    wimg.Clear(System.Windows.Media.Color.FromRgb(255, 0, 0));
            //    wimg.DrawLine(0, 0, 100, 100, System.Windows.Media.Color.FromRgb(255, 255, 0));
            //    wimg.DrawEllipse(50, 50, 20, 100, System.Windows.Media.Color.FromRgb(255, 255, 255));
            //    });
            //    tasks[i].Start();

            //}
            //Task.WaitAll(tasks);

            //MessageBox.Show(String.Format("{0} ms", sw.ElapsedMilliseconds));
            for (int i = 0; i < 1000; i++)
            {
                FDLTests.test(false);
            }
            MessageBox.Show(String.Format("{0} ms", sw.ElapsedMilliseconds));

            sw.Restart();
            this.pictureBox1.Image = FDLTests.test(true);
            MessageBox.Show(String.Format("{0} ms", sw.ElapsedMilliseconds));


            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {

                using (Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Red);
                    g.DrawLine(Pens.AliceBlue, 0, 0, 100, 100);
                    g.DrawEllipse(Pens.AliceBlue, 50, 50, 20, 100);
                }

                //}
            }
            //Task.WaitAll(tasks);

            MessageBox.Show(String.Format("{0} ms", sw.ElapsedMilliseconds));



            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {

                using (Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.Clear(System.Drawing.Color.Red);
                    g.DrawLine(Pens.AliceBlue, 0, 0, 100, 100);
                    g.DrawEllipse(Pens.AliceBlue, 50, 50, 20, 100);
                }

                //}
            }
                //Task.WaitAll(tasks);

                MessageBox.Show(String.Format("{0} ms", sw.ElapsedMilliseconds));


                //this.pictureBox1.Image = img.ToBitmap();
            }

        private void button2_Click(object sender, EventArgs e)
        {
            CogImage8Grey cogimg = new CogImage8Grey(new Bitmap("test.bmp"));

            Stopwatch sw = Stopwatch.StartNew();
            this.pictureBox1.Image = cogimg.ToBitmap();
            MessageBox.Show("cog to bitmap:" + sw.ElapsedMilliseconds.ToString());

            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                    Bitmap nofnol = cogimg.ToBitmap();
            }
            MessageBox.Show("cog to bitmap:" + (sw.ElapsedMilliseconds / 100).ToString());

            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                FDLImage nofnol = new FDLImage(cogimg);
            }
            MessageBox.Show("cog to fdl:" + (sw.ElapsedMilliseconds / 100).ToString());


            sw.Restart();
            FDLImage img = null;
            for (int i = 0; i < 100; i++)
            {
                img = new FDLImage(cogimg);
                using (FDLFont font = new FDLFont(Color.Green, "Arial", 30, FDLFontStyle.Normal))
                using (FDLBrush brush = new FDLBrush(Color.Aqua, 1))
                using (FDLBrush brush2 = new FDLBrush(Color.Red, 5))
                {
                    img.Rotate(-1.3f);
                    img.DrawText(30, 30, "Szöveg", font);
                    img.DrawEllipse(100, 100, 50, 100, brush);
                    img.DrawLine(0, 0, 100, 100, brush2);
                }

                CogImage8Grey bmp2 = img.ToCogImage8();
            }
            MessageBox.Show("Width FDL: " + (sw.ElapsedMilliseconds / 100).ToString());

            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                Bitmap bmp = cogimg.ToBitmap();
                Bitmap tmpbmp = new Bitmap(bmp.Width, bmp.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {

                    //turn the Bitmap into a Graphics object
                    Graphics gfx = Graphics.FromImage(tmpbmp);

                    //now we set the rotation point to the center of our image
                    gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                    gfx.RotateTransform(-1.3f);
                    gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
                    gfx.DrawImage(bmp, new Point(0, 0));
                    gfx.RotateTransform(1.3f);
                    gfx.DrawEllipse(Pens.Red, 100, 100, 50, 100);
                    gfx.DrawString("Szöveg", SystemFonts.DefaultFont, Brushes.Red, 30, 30);
                    gfx.DrawLine(Pens.Red, 0, 0, 100, 100);
                }
                CogImage8Grey test = new CogImage8Grey(tmpbmp);
            }
            MessageBox.Show("Width GDI+: " + (sw.ElapsedMilliseconds / 100).ToString());

            Bitmap bmptest = img.ToBitmap();
            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                CogImage8Grey nofnol = new CogImage8Grey(bmptest);
            }
            MessageBox.Show("gdi to cog:" + (sw.ElapsedMilliseconds).ToString());

            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                CogImage8Grey nofnol = img.ToCogImage8();
            }
            MessageBox.Show("fdkl to cog:" + (sw.ElapsedMilliseconds).ToString());


            this.pictureBox2.Image = img.ToBitmap();

        }
    }
}
