using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode_2020
{
    class Day1
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day1.txt";
            List<int> array = new List<int>();

            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                    array.Add(int.Parse(line));
            }

            for (var i = 0; i < array.Count; ++i)
            {
                for (var j = i + 1; j < array.Count; ++j)
                {
                    for (var k = j + 1; k < array.Count; ++k)
                    {
                        if (array[i] + array[j] + array[k] == 2020)
                        {
                            Console.WriteLine("Multiplication: " + array[i] * array[j] * array[k]);
                            return;
                        }
                    }
                }
            }
        }
    }
}
