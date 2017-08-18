using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.FDL
{
    public class FDLFont : FDLPaint
    {
        public FDLFont(Color c, string fontName, int size, FDLFontStyle style)
        {
            this.l_Paint = new SKPaint()
            {
                Color = new SKColor(c.R, c.G, c.B, c.A),
                Typeface = SKTypeface.FromFamilyName(fontName, (SKTypefaceStyle)style),
                TextSize = size,
                TextEncoding = SKTextEncoding.Utf8,
                TextAlign = SKTextAlign.Left            
            };
        }
    }
}
