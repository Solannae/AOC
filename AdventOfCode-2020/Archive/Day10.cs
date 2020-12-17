using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode_2020.Archive
{
    class Day10
    {
        private static long[] possiblePaths;

        public static void Run()
        {
            const string path = @"../../../Inputs/day10.txt";

            using (var stream = new StreamReader(path))
            {
                string line;
                var adapters = new List<int>();

                while ((line = stream.ReadLine()) != null)
                {
                    adapters.Add(int.Parse(line));
                }

                adapters.Add(0);
                adapters.Add(adapters.Max() + 3);

                var array = adapters.OrderBy(u => u).ToArray();
                Precalculate(array);

                Console.WriteLine($"Possible paths: {possiblePaths[0]}.");
            }
        }

        private static void Precalculate(int[] array)
        {
            possiblePaths = new long[array.Length];
            possiblePaths[array.Length - 1] = 1;

            for (var i = array.Length - 1; i >= 0; --i)
            {
                var reachable = new List<long>();

                for (var j = i + 1; j < array.Length; ++j)
                {
                    if (array[j] - array[i] > 3)
                        break;

                    reachable.Add(j);
                }
                
                foreach (var path in reachable)
                {
                    possiblePaths[i] += possiblePaths[path];
                }
            }
        }
    }
}
