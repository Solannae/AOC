using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Archive
{
    class Day7
    {
        public static void Run()
        {
            var path = @"../../../Inputs/day7.txt";
            int[] array;

            using (var stream = new StreamReader(path))
            {
                var line = stream.ReadLine();
                array = line.Split(",").Select(int.Parse).ToArray();
            }

            var amplifiers = 5;
            var possibilities = BuildPossibilities(amplifiers);
            var computerList = new List<IntCode>();
            var max = 0;

            foreach (var setting in possibilities)
            {
                computerList.Clear();

                for (var i = 0; i < amplifiers; ++i)
                {
                    computerList.Add(new IntCode(array));
                    computerList[i].PushToBuffer(int.Parse(setting[i].ToString()));
                }

                var input = 0;

                do
                {
                    for (var i = 0; i < computerList.Count; ++i)
                    {
                        computerList[i].PushToBuffer(input);
                        computerList[i].RunIntcode();
                        input = computerList[i].Register;
                    }

                } while (computerList.Any(u => u.IsRunning));

                if (max < computerList[computerList.Count() - 1].Register)
                    max = computerList[computerList.Count() - 1].Register;
            }

            Console.WriteLine(max);
        }

        public static List<string> BuildPossibilities(int size)
        {
            var range = Enumerable.Range(5, size).ToList();

            var list = new List<string>();
            list.AddRange(BuildPossibilitiesRec(range, ""));

            return list;
        }

        public static List<string> BuildPossibilitiesRec(List<int> array, string currentPattern)
        {
            var list = new List<string>();

            for (var i = 0; i < array.Count; ++i)
            {
                var tmp = new List<int>(array);
                var tmpPattern = new StringBuilder(currentPattern);
                tmpPattern.Append(tmp[i]);
                tmp.RemoveAt(i);

                list.AddRange(BuildPossibilitiesRec(tmp, tmpPattern.ToString()));
            }

            if (array.Count == 0)
                list.Add(currentPattern.ToString());

            return list;
        }
    }
}
