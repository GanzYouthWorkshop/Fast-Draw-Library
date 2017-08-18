using Cognex.VisionPro;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEV.FDL
{
    public class FDLImage : IDisposable
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        private SKBitmap m_Bitmap;
        private SKCanvas m_Canvas;

        public int Width { get; }
        public int Height { get; }

        public FDLImage(int width, int height)
        {
            this.m_Bitmap = new SKBitmap(new SKImageInfo(width, height, SKColorType.Bgra8888, SKAlphaType.Premul));
            this.m_Canvas = new SKCanvas(this.m_Bitmap);

            this.Width = width;
            this.Height = height;
        }

        public FDLImage(Bitmap bitmap)
        {
            //TODO: [KG] Pixel copy-val is meg lehet oldani!
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.MemoryBmp);
                this.m_Bitmap = SKBitmap.Decode(stream);

                this.Width = this.m_Bitmap.Width;
                this.Height = this.m_Bitmap.Height;
            }
        }

        public FDLImage(CogImage8Grey cogimage)
        {
            try
            {
                ICogImage8PixelMemory pixels = cogimage.Get8GreyPixelMemory(CogImageDataModeConstants.Read, 0, 0, cogimage.Width, cogimage.Height);

                SKImage im = SKImage.FromPixelCopy(new SKImageInfo(cogimage.Width, cogimage.Height, SKColorType.Gray8, SKAlphaType.Opaque), pixels.Scan0, pixels.Stride);
                this.m_Bitmap = SKBitmap.FromImage(im);
                this.m_Canvas = new SKCanvas(this.m_Bitmap);

                this.Width = pixels.Width;
                this.Height = pixels.Height;

                pixels.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        public void Rotate(float degrees)
        {
            SKBitmap oldbmp = this.m_Bitmap;
            SKCanvas oldcanvas = this.m_Canvas;

            SKBitmap newBitmap = new SKBitmap(this.m_Bitmap.Width, this.m_Bitmap.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
            SKCanvas newCanvas = new SKCanvas(newBitmap);

            int px = this.m_Bitmap.Width / 2;
            int py = this.m_Bitmap.Height / 2;

            newCanvas.Clear(new SKColor(255,255,255,255));
            newCanvas.RotateDegrees(degrees, px, py);
            newCanvas.DrawBitmap(this.m_Bitmap, 0, 0, new SKPaint() { IsAntialias = true });
            newCanvas.RotateDegrees(-degrees, px, py);

            this.m_Bitmap = newBitmap;
            this.m_Canvas = newCanvas;

            oldbmp.Dispose();
            oldcanvas.Dispose();
        }

        public void DrawLine(int x1, int y1, int x2, int y2, FDLBrush brush)
        {
            this.m_Canvas.DrawLine(x1, y1, x2, y2, brush.l_Paint);
        }

        public void DrawEllipse(int x0, int y0, int rx, int ry, FDLBrush brush)
        {
            this.m_Canvas.DrawOval(x0, y0, ry, ry, brush.l_Paint);
        }

        public void DrawText(int x, int y, string text, FDLFont font)
        {
            this.m_Canvas.DrawText(text, x, y, font.l_Paint);
        }


        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppPArgb);

            this.m_Bitmap.LockPixels();
            var bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            CopyMemory(bd.Scan0, this.m_Bitmap.GetPixels(), (uint)(this.m_Bitmap.RowBytes  * this.Height));
            bmp.UnlockBits(bd);
            this.m_Bitmap.UnlockPixels();
            return bmp;
        }

        public CogImage8Grey ToCogImage8()
        {
            return new CogImage8Grey(this.ToBitmap());
        }

        public CogImage24PlanarColor ToCogImage24()
        {
            return new CogImage24PlanarColor(this.ToBitmap());
        }

        public void Dispose()
        {
            this.m_Bitmap.Dispose();
            this.m_Canvas.Dispose();
        }
    }
}
