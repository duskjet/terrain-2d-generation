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
        PixelMap map;

        public PixelMap Generate(CellularAutomataOptions options) => GenerateMap(options);

        private PixelMap GenerateRandomMap(CellularAutomataOptions options)
        {
            var map = new PixelMap(options);

            var random = new System.Random();

            int anal = 0;

            for (int x = 0; x < options.Width; x++)
                for (int y = 0; y < options.Height; y++)
                {
                    map.Set(x, y, IntToBoolean(random.Next(-1, 100)));
                    if (map[x, y]) anal++;
                }

            var alivecells = anal * 100 / (options.Width * options.Height);

            return map;
        }

        private bool IntToBoolean(int value) => value < 50;

        private PixelMap GenerateMap(CellularAutomataOptions options)
        {
            if (map == null)
            {
                map = GenerateRandomMap(options);
                return map;
            }

            var mapCopy = new PixelMap(options);

            for (int x = 1; x < options.Width - 1; x++)
                for (int y = 1; y < options.Height - 1; y++)
                {
                    var neighbors = new bool[] {
                        map[x - 1, y - 1],
                        map[x - 1, y + 1],
                        map[x + 1, y - 1],
                        map[x + 1, y + 1],
                        map[x, y + 1],
                        map[x, y - 1],
                        map[x - 1, y],
                        map[x + 1, y]
                    };

                    var aliveNeighbors = neighbors.Count(alive => alive == true);

                    if (!map[x, y] && options.Born.Contains(aliveNeighbors) || map[x, y] && options.Survive.Contains(aliveNeighbors))
                        mapCopy.Set(x, y, true);

                    //if (options.Survive.Contains(aliveNeighbors))
                    //    mapCopy.Set(x, y, true);
                    //else
                    //    mapCopy.Set(x, y, false);
                }

            map = mapCopy;
            return map;
        }
    }

    public class CellularAutomataOptions : GraphicOptions
    {
        public int[] Born { get; set; }
        public int Iterations { get; set; }
        public int[] Survive { get; set; }
    }
}
