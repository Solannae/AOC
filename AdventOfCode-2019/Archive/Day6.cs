using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Archive
{
    class Day6
    {
        public static void Run()
        {
            var path = @"../../../Inputs/day6.txt";
            var nodes = new List<Node>();
            string line;

            using (var stream = new StreamReader(path))
            {
                while ((line = stream.ReadLine()) != null)
                {
                    var child = line.Split(")")[1];
                    var parent = line.Split(")")[0];

                    var childNode = nodes.FirstOrDefault(u => u.Label == child);
                    var parentNode = nodes.FirstOrDefault(u => u.Label == parent);

                    if (childNode == null)
                    {
                        childNode = new Node(child);
                        nodes.Add(childNode);
                    }

                    if (parentNode == null)
                    {
                        parentNode = new Node(parent);
                        nodes.Add(parentNode);
                    }

                    parentNode.Children.Add(childNode);
                    childNode.Parent = parentNode;
                }
            }

            var root = nodes.First(u => !nodes.Any(v => v.Children.Contains(u)));
            var you = nodes.First(u => u.Label == "YOU");
            var santa = nodes.First(u => u.Label == "SAN");

            Console.WriteLine(Node.GetSumOfDepth(root, 0));
            Console.WriteLine(Node.GetOrbitalTransfers(you, santa));
        }
    }
}
