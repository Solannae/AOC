using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode_2020.Archive
{
    public struct Node
    {
        public Node(string name)
        {
            this.Name = name;
            LinkedNodes = new List<Tuple<Node, int>>();
        }

        public string Name;
        public List<Tuple<Node, int>> LinkedNodes;
    }

    class Day7
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day7.txt";

            using (var stream = new StreamReader(path))
            {
                List<Node> nodes = new List<Node>();

                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    ParseLine(nodes, line);
                }

                var query = "shiny gold";
                var compatible = 0;

                foreach (var node in nodes)
                {
                    if (CheckNestedBags(node, query))
                        ++compatible;
                }

                Console.WriteLine($"Amount of bags compatible with the color {query}: {compatible}.");
            }
        }

        private static Node ParseLine(List<Node> nodes, string line)
        {
            var splitInput = line.Split(" bags contain ");

            var node = nodes.FirstOrDefault(u => u.Name == splitInput[0]);
            if (node.Equals(default(Node)))
            {
                node = new Node(splitInput[0]);
                nodes.Add(node);
            }

            var containedBags = splitInput[1].Replace(".", "");

            foreach (var bag in containedBags.Split(", "))
            {
                if (bag.Contains("no other"))
                    continue;

                var temp = bag.Replace(" bags", "");
                temp = temp.Replace(" bag", "");

                var firstSpace = temp.IndexOf(' ');
                var amount = int.Parse(temp.Substring(0, firstSpace));
                var bagName = temp.Substring(firstSpace + 1, temp.Length - firstSpace - 1);

                var childNode = nodes.FirstOrDefault(u => u.Name == bagName);
                if (childNode.Equals(default(Node)))
                {
                    childNode = new Node(bagName);
                    nodes.Add(childNode);
                }

                node.LinkedNodes.Add(new Tuple<Node, int>(childNode, amount));
            }

            return node;
        }

        private static void PrettyPrint(Node node)
        {
            Console.Write($"The bag {node.Name} contains (");
            foreach (var bag in node.LinkedNodes)
            {
                Console.Write($"{bag.Item2} {bag.Item1.Name},");
            }
            Console.Write(").\n");
        }

        private static bool CheckNestedBags(Node node, string query, int depth = 0)
        {
            if (node.Name == query)
            {
                return (depth != 0);
            }

            var found = false;
            foreach (var childrenNode in node.LinkedNodes)
            {
                found = found || CheckNestedBags(childrenNode.Item1, query, depth + 1);
            }

            return found;
        }
    }
}
