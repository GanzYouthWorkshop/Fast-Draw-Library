using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.FDL
{
    public class FDLBrush : FDLPaint
    {
        public FDLBrush(Color c, int width)
        {
            this.l_Paint = new SKPaint()
            {
                IsStroke = true,
                Color = new SKColor(c.R, c.G, c.B, c.A),
                StrokeWidth = width
            };
        }
    }
}
