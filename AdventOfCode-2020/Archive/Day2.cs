using System;
using System.IO;

namespace AdventOfCode_2020.Archive
{
    public class Day2
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day2.txt";

            using (var stream = new StreamReader(path))
            {
                var correctPasswords = 0;

                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var input = line.Split(" ");
                    var requiredIndexes = input[0];
                    var requiredLetter = input[1][0];
                    var password = input[2];

                    var firstIndex = int.Parse(requiredIndexes.Split("-")[0]);
                    var secondIndex = int.Parse(requiredIndexes.Split("-")[1]);

                    int occurrences = 0;
                    if (password[firstIndex - 1] == requiredLetter)
                        ++occurrences;
                    if (password[secondIndex - 1] == requiredLetter)
                        ++occurrences;

                    if (occurrences == 1)
                        ++correctPasswords;
                }

                Console.WriteLine("Correct Passwords: " + correctPasswords);
            }
        }
    }
}
