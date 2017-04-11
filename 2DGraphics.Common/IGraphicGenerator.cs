using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Common
{
    public interface IGraphicGenerator<TOptions> where TOptions : GraphicOptions
    {
        PixelMap Generate(TOptions options);
    }
}
