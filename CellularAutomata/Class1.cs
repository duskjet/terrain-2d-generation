using Graphics.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.CellularAutomata
{
    public class CellularAutomata : IGraphicGenerator<CellularAutomataOptions>
    {
        public PixelMap Generate(CellularAutomataOptions options) => GenerateRandomMap(options);

        private PixelMap GenerateRandomMap(CellularAutomataOptions options)
        {
            var map = new PixelMap(options);

            var random = new System.Random();

            for (int x = 0; x < options.Width; x++)
                for (int y = 0; y < options.Height; y++)
                {
                    map.Set(x, y, IntToBoolean(random.Next(0, 2)));
                }

            return map;
        }

        private bool IntToBoolean(int value) => value == 1;
    }

    public class CellularAutomataOptions : GraphicOptions
    {
    }
}
