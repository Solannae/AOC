using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode_2020.Archive
{
    class Day6
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day6.txt";

            using (var stream = new StreamReader(path))
            {
                string line;
                var sumYes = 0;

                while ((line = stream.ReadLine()) != null)
                {
                    List<HashSet<char>> currentGroupYes = new List<HashSet<char>>();

                    do
                    {
                        HashSet<char> currentPerson = new HashSet<char>();

                        foreach (var c in line)
                        {
                            currentPerson.Add(c);
                        }

                        currentGroupYes.Add(currentPerson);

                        line = stream.ReadLine();
                    } while (line != null && line != string.Empty);

                    foreach (var answer in currentGroupYes[0])
                    {
                        var similarAnswers = 0;

                        for (var i = 1; i < currentGroupYes.Count; ++i)
                        {
                            if (currentGroupYes[i].Contains(answer))
                                ++similarAnswers;
                        }

                        if (similarAnswers == currentGroupYes.Count - 1)
                            ++sumYes;
                    }
                }

                Console.WriteLine("Number of questions to which everyone answered yes in groups: " + sumYes);
            }
        }
    }
}
