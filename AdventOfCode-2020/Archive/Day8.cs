using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AdventOfCode_2020.Libs;

namespace AdventOfCode_2020.Archive
{
    class Day8
    {
        public static void Run()
        {
            const string path = @"../../../Inputs/day8.txt";
            List<Instruction> sequence = new List<Instruction>();

            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    sequence.Add(Instruction.ParseInstruction(line));
                }
            }

            for (var i = 0; i < sequence.Count; ++i)
            {
                switch (sequence[i].Name)
                {
                    case "nop":
                    case "jmp":
                        var duplicate = sequence.Select(inst => new Instruction(inst.Name, inst.Value)).ToList();
                        duplicate[i].Name = (sequence[i].Name == "jmp" ? "nop" : "jmp");

                        Boot(duplicate);
                        break;

                    default:
                        continue;
                }
            }
        }

        private static bool Boot(List<Instruction> bootSequence)
        {
            var index = 0;
            var accumulator = 0;

            while (index < bootSequence.Count && !bootSequence[index].Executed)
            {
                bootSequence[index].Executed = true;
             
                switch (bootSequence[index].Name)
                {
                    case "nop":
                        ++index;
                        break;

                    case "acc":
                        accumulator += bootSequence[index].Value;
                        ++index;
                        break;

                    case "jmp":
                        index += bootSequence[index].Value;
                        break;
                }
            }

            if (index >= bootSequence.Count)
            {
                Console.WriteLine($"Boot succeded. Accumulator value at the end of bootsequence: {accumulator}.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
