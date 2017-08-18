using SkiaSharp;
using SkiaSharp.Extended;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.FDL
{
    public class FDLTests
    {
        public static Bitmap test(bool save)
        {
            Bitmap tmp = null;
            //SKStream stream = null; ;
            using (SKBitmap bmp = new SKBitmap(new SKImageInfo(320, 240, SKColorType.Gray8)))
            using (SKCanvas canvas = new SKCanvas(bmp))
            {
                using (SKPaint paint = new SKPaint())
                {
                    paint.Color = new SKColor(255, 255, 255);
                    paint.IsStroke = true;

                    canvas.Clear(new SKColor(120, 120, 120));
                    canvas.DrawLine(0, 0, 100, 100, paint);
                    canvas.DrawOval(20, 30, 40, 50, paint);
                    canvas.Flush();
                    canvas.RotateDegrees(0.22f);
                }

                if (save)
                {
                    using (SKImage img = SKImage.FromBitmap(bmp))
                    using (MemoryStream s = new MemoryStream())
                    using (SKData data = img.ToRasterImage().Encode(SKEncodedImageFormat.Png, 100))
                    {
                        tmp = new Bitmap(data.AsStream());
                    }
                }
                else
                {
                    tmp = null;
                }
            }
            return tmp;
        }
    }
}
