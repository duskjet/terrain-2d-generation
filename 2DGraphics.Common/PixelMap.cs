using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Graphics.Common
{
    public class PixelMap
    {
        bool[,] map;

        public GraphicOptions Options { get; }

        public PixelMap(GraphicOptions options)
        {
            Options = options;

            map = new bool[options.Width, options.Height];
        }

        public void Set(int x, int y, bool value)
        {
            map[x, y] = value;
        }

        public bool this[int x, int y]
        {
            get { return map[x, y]; }
        }
    }

    public static class PixelMapExtensions
    {
        public static WriteableBitmap ToWriteableBitmap(this PixelMap map)
        {
            var width = map.Options.Width;
            var height = map.Options.Height;

            var bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);

            // Create an array of pixels to contain pixel color values
            uint[] pixels = new uint[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int i = width * y + x;

                    if (map[x, y])
                    {
                        pixels[i] = (uint)((0 << 16) + (0 << 8) + 0);
                    }
                    else
                    {
                        pixels[i] = (uint)((255 << 16) + (255 << 8) + 255);
                    }

                }
            }

            // apply pixels to bitmap
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, 0);

            return bitmap;
        }
    }
}
