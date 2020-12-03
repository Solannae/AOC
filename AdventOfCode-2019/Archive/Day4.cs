using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Archive
{
    class Day4
    {
        public static void Run()
        {
            const int min = 265275;
            const int max = 781584;

            var list = Enumerable.Range(min, max - min);
            list = list.Where(u => HasDouble(u) && DigitsNotDecreasing(u));
            Console.WriteLine(list.Count());
        }

        public static bool HasDouble(int nb)
        {
            var array = nb.ToString();
            var hasDouble = false;
            var lastDigit = array[0];
            var streak = 1;


            for (var i = 1; i < array.Length; ++i)
            {
                if (array[i] == lastDigit)
                {
                    ++streak;
                }
                else
                {
                    if (streak == 2)
                        hasDouble = true;
                    lastDigit = array[i];
                    streak = 1;
                }
            }

            if (streak == 2)
                hasDouble = true;

            return hasDouble;
        }

        public static bool DigitsNotDecreasing(int nb)
        {
            var array = nb.ToString();
            for (var i = 0; i < array.Length - 1; ++i)
            {
                if (array[i] > array[i + 1])
                    return false;
            }

            return true;
        }
    }
}
