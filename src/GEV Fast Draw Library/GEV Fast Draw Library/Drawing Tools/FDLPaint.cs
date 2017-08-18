using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.FDL
{
    public abstract class FDLPaint : IDisposable
    {
        internal SKPaint l_Paint;

        public void Dispose()
        {
            this.l_Paint.Dispose();
        }
    }
}
