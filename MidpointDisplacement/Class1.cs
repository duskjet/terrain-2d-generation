using Graphics.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Graphics.MidpointDisplacement
{
    public class MidpointDisplacement : IGraphicGenerator<MidpointDisplacementOptions>
    {
        Random random = new Random();

        public PixelMap Generate(MidpointDisplacementOptions options) => GenerateMap(options);

        private PixelMap GenerateRandomMap(MidpointDisplacementOptions options)
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

        private PixelMap GenerateMap(MidpointDisplacementOptions options)
        {
            var map = new PixelMap(options);

            var startPoint = new Point(0, options.Height / 2);
            var endPoint = new Point(options.Width, options.Height / 2);
            var line = new List<Point>() { startPoint, endPoint };

            // recursive method
            DisplacePoints(line, 90, 0.5, 10);

            using (var bitmap = new Bitmap(options.Width, options.Height))
            using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                // connect all points
                graphics.DrawLines(new Pen(Color.Black, 1), line.ToArray());

                // convert bitmap to pixel map
                for (int x = 0; x < options.Width; x++)
                    for (int y = 0; y < options.Height; y++)
                        map.Set(x, y, bitmap.GetPixel(x, y).A != 0);
            }

            return map;
        }

        private void DisplacePoints(List<Point> line, double range, double roughness, int steps)
        {
            for (int i = 0; i < line.Count - 1; i += 2)
            {
                var heightOffset = GetRandomDouble(-range, range);

                var midpoint = GetMidpoint(line[i], line[i + 1], (int)heightOffset);

                line.Insert(i + 1, midpoint);
            }

            // reduce height offset for the next step
            range *= roughness;

            // continue recursion
            steps--;
            if (steps > 0) DisplacePoints(line, range, roughness, steps);
        }

        private double GetRandomDouble(double min, double max) => random.NextDouble() * (max - min) + min;

        private Point GetMidpoint(Point start, Point end, int heightOffset) => new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2 + heightOffset);
    }

    public class MidpointDisplacementOptions : GraphicOptions
    {
        public int RecursionDepth { get; set; }
        public float Roughness { get; set; }
    }
}
