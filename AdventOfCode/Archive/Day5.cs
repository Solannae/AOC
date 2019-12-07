using System.IO;
using System.Linq;

namespace AdventOfCode.Archive
{
    public class Day5
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day5.txt";
            int[] array;

            using (var stream = new StreamReader(path))
            {
                var line = stream.ReadLine();
                array = line.Split(",").Select(u => int.Parse(u)).ToArray();
            }

            IntCode computer = new IntCode(array);
            computer.RunIntcode();
        }
    }
}
