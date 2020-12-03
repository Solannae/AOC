using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Node
    {
        public string Label { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }

        public Node(string label)
        {
            this.Label = label;
            this.Children = new List<Node>();
        }

        public static int GetSumOfDepth(Node node, int currentFloor)
        {
            var sum = currentFloor;

            foreach (var child in node.Children)
            {
                sum += GetSumOfDepth(child, currentFloor + 1);
            }

            return sum;
        }

        public static int GetOrbitalTransfers(Node a, Node b)
        {
            var distance = 0;

            var parentsOfNodeA = new List<Node>();

            var currentNode = a.Parent;
            while (currentNode != null && currentNode != b)
            {
                parentsOfNodeA.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            if (currentNode == b)
                return parentsOfNodeA.Count - 1;

            var step = 0;
            currentNode = b.Parent;
            while (!parentsOfNodeA.Contains(currentNode) && currentNode != null && currentNode != a)
            {
                currentNode = currentNode.Parent;
                ++step;
            }

            distance += step;
            
            if (currentNode != a)
                distance += parentsOfNodeA.IndexOf(currentNode);

            return distance;
        }
    }
}
