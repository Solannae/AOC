using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode_2020.Archive
{
    class Day5
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day5.txt";
            var idList = new List<int>();

            using (var stream = new StreamReader(path))
            {
                string line;
                var highestId = 0;

                while ((line = stream.ReadLine()) != null)
                {
                    var lowerHalf = 0;
                    var upperHalf = 127;

                    int row;
                    int column;
                    int seatId;

                    var step = 0;
                    for (; step < 7; ++step)
                    {
                        var result = Split(lowerHalf, upperHalf, line[step]);
                        lowerHalf = result.Item1;
                        upperHalf = result.Item2;
                    }

                    row = lowerHalf;
                    lowerHalf = 0;
                    upperHalf = 8;

                    for (; step < line.Length; ++step)
                    {
                        var result = Split(lowerHalf, upperHalf, line[step]);
                        lowerHalf = result.Item1;
                        upperHalf = result.Item2;
                    }

                    column = lowerHalf;
                    seatId = row * 8 + column;

                    if (seatId > highestId)
                        highestId = seatId;

                    idList.Add(seatId);
                }

                idList.Sort();
                var prevSeat = 0;

                for (var i = 1; i < idList.Count - 1; ++i)
                {
                    if (idList[i] - prevSeat == 2)
                        Console.WriteLine("Our seat has ID: " + (idList[i] + prevSeat) / 2);

                    prevSeat = idList[i];
                }
            }
        }

        private static Tuple<int, int> Split(int lower, int upper, char delim)
        {
            if (delim == 'F' || delim == 'L')
            {
                upper = (int)Math.Floor(((double)lower + upper) / 2);
            }
            else if (delim == 'B' || delim == 'R')
            {
                lower = (int)Math.Ceiling(((double)lower + upper) / 2);
            }

            return new Tuple<int, int>(lower, upper);
        }
    }
}
