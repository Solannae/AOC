using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Archive
{
    public class Day2
    {
        static void Run()
        {
            const string path = @"../../../Inputs/day2.txt";
            var inputs = new List<int>();
            int[] array;

            using (var stream = new StreamReader(path))
            {
                var line = stream.ReadLine();
                array = line.Split(",").Select(u => int.Parse(u)).ToArray();
            }

            for (var i = 0; i < 99; ++i)
            {
                for (var j = 0; j < 99; ++j)
                {
                    var tmpArray = (int[])array.Clone();
                    tmpArray[1] = i;
                    tmpArray[2] = j;
                    RunIntcode(tmpArray);
                    if (tmpArray[0] == 19690720)
                        Console.WriteLine(100 * tmpArray[1] + tmpArray[2]);
                }
            }
        }

        public static void RunIntcode(int[] array)
        {
            var index = 0;

            while (array[index] != 99)
            {
                switch (array[index])
                {
                    case 1:
                        array[array[index + 3]] = array[array[index + 1]] + array[array[index + 2]];
                        break;
                    case 2:
                        array[array[index + 3]] = array[array[index + 1]] * array[array[index + 2]];
                        break;
                    default:
                        return;
                }

                index += 4;
            }
        }
    }
}
