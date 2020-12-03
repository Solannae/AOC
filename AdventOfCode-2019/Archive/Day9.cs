using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Archive
{
    class Day9
    {
        public static void Run()
        {
            var path = @"../../../Inputs/day9.txt";
            long[] array;

            using (var reader = new StreamReader(path))
            {
                array = reader.ReadLine().Split(",").Select(long.Parse).ToArray();
            }

            var computer = new IntCode(array);
            computer.PushToBuffer(1);
            computer.RunIntcode();
        }
    }
}
