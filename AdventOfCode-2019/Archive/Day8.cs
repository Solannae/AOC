using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode.Archive
{
    class Day8
    {
        public static void Run()
        {
            var path = "../../../Inputs/day8.txt";
            var output = "answer.bmp";

            string line;

            using (var stream = new StreamReader(path))
            {
                line = stream.ReadLine();
            }

            var width = 25;
            var height = 6;
            var layers = new List<string>();

            for (var i = 0; i < line.Length; i += width * height)
            {
                layers.Add(line.Substring(i, width * height));
            }

            CorruptionCheck(layers);

            var bitmap = BuildImage(layers, width, height);

            bitmap.Bitmap.Save(output);
        }

        public static void CorruptionCheck(List<string> layers)
        {
            var fewestZeros = layers.OrderBy(u => u.Where(v => v == '0').Count()).First();
            var ones = fewestZeros.Where(u => u == '1').Count();
            var twos = fewestZeros.Where(u => u == '2').Count();

            Console.WriteLine(ones * twos);
        }

        public static FastBitmap BuildImage(List<string> layers, int width, int height)
        {
            var bitmap = new FastBitmap(width, height);

            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    for (var k = 0; k < layers.Count; ++k)
                    {
                        var pixel = layers[k][i * width + j];

                        if (pixel == '2')
                            continue;

                        var color = (pixel == '0' ? Color.Black : Color.White);
                        bitmap.SetPixel(j, i, color);
                        break;
                    }
                }
            }

            return bitmap;
        }
    }
}
