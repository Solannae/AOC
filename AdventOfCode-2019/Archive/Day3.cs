using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Archive
{
    class Day3
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day3.txt";
            using (var reader = new StreamReader(path))
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                var line1 = reader.ReadLine();
                var line2 = reader.ReadLine();

                var wire1 = GenerateSet(line1);
                var wire2 = GenerateSet(line2);

                var intersection = wire1.Intersect(wire2);

                var distances = intersection.Select(u => wire1.IndexOf(u) + 1 + wire2.IndexOf(u) + 1);
                Console.WriteLine(distances.OrderBy(u => u).First());

                watch.Stop();
                Console.Write(watch.Elapsed);
            }
        }

        public static List<Point> GenerateSet(string line)
        {
            var set = new List<Point>();
            var array = line.Split(",");

            var x = 0;
            var y = 0;

            foreach (var item in array)
            {
                var coefX = 0;
                var coefY = 0;

                switch (item[0])
                {
                    case 'R':
                        coefX = 1;
                        break;
                    case 'L':
                        coefX = -1;
                        break;
                    case 'U':
                        coefY = -1;
                        break;
                    case 'D':
                        coefY = 1;
                        break;
                }

                for (var i = 0; i < int.Parse(item.Substring(1)); ++i)
                {
                    set.Add(new Point(x + coefX, y + coefY));
                    x += coefX;
                    y += coefY;
                }
            }

            return set;
        }

        public static int ManhattanDistance(Point a)
        {
            return Math.Abs(a.X) + Math.Abs(a.Y);
        }
    }
}
