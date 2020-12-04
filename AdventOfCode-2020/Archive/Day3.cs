using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode_2020.Archive
{
    public class Day3
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day3.txt";

            List<string> mountain = new List<string>();

            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    mountain.Add(line);
                }
            }

            var rightOffsets = new int[] { 1, 3, 5, 7, 1 };
            var downOffsets = new int[] { 1, 1, 1, 1, 2 };
            long multipliedTrees = 1;

            for (var i = 0; i < rightOffsets.Length; ++i)
            {
                var xPosition = 0;
                var yPosition = 0;
                var trees = 0;

                while (yPosition < mountain.Count - downOffsets[i])
                {
                    xPosition += rightOffsets[i];
                    yPosition += downOffsets[i];

                    if (mountain[yPosition][xPosition % mountain[yPosition].Length] == '#')
                        ++trees;
                }

                Console.WriteLine($"Trees for ({rightOffsets[i]}, {downOffsets[i]}): {trees}");

                multipliedTrees *= trees;
            }

            Console.WriteLine("Multiplied Trees: " + multipliedTrees);
        }
    }
}
