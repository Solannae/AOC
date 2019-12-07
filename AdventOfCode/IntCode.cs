using System;

namespace AdventOfCode
{
    class IntCode
    {
        private readonly int[] Rom;
        public int[] Ram { get; private set; }
        public int Ptr { get; private set; }

        public IntCode(int[] array)
        {
            this.Ram = (int[])array.Clone();
            this.Rom = (int[])array.Clone();
            Ptr = 0;
        }

        public void Reset()
        {
            Ram = (int[])Rom.Clone();
            Ptr = 0;
        }

        public void RunIntcode()
        {
            while (Ram[Ptr] != 99)
            {
                var opcode = Ram[Ptr] % 100;

                switch (opcode)
                {
                    // Add
                    case 1:
                        Ram[Ram[Ptr + 3]] = GetParameter(1) + GetParameter(2);
                        Ptr += 4;
                        break;

                    // Multiply
                    case 2:
                        Ram[Ram[Ptr + 3]] = GetParameter(1) * GetParameter(2);
                        Ptr += 4;
                        break;

                    // ReadInput
                    case 3:
                        var line = Console.ReadLine();
                        if (int.TryParse(line, out var nb))
                            Ram[Ram[Ptr + 1]] = nb;
                        Ptr += 2;
                        break;

                    // WriteOutput
                    case 4:
                        Console.WriteLine(GetParameter(1));
                        Ptr += 2;
                        break;

                    // JumpIfTrue
                    case 5:
                        if (GetParameter(1) != 0)
                            Ptr = GetParameter(2);
                        else
                            Ptr += 3;
                        break;

                    // JumpIfFalse
                    case 6:
                        if (GetParameter(1) == 0)
                            Ptr = GetParameter(2);
                        else
                            Ptr += 3;
                        break;

                    // LessThan
                    case 7:
                        if (GetParameter(1) < GetParameter(2))
                            Ram[Ram[Ptr + 3]] = 1;
                        else
                            Ram[Ram[Ptr + 3]] = 0;
                        Ptr += 4;
                        break;

                    // Equal
                    case 8:
                        if (GetParameter(1) == GetParameter(2))
                            Ram[Ram[Ptr + 3]] = 1;
                        else
                            Ram[Ram[Ptr + 3]] = 0;
                        Ptr += 4;
                        break;

                    default:
                        return;
                }
            }
        }

        public int GetParameter(int index)
        {
            var debug = Math.Pow(10, index - 1);
            var isImmediate = (int)(Ram[Ptr] % (1000 * debug) / (100 * debug)) == 1;

            if (isImmediate)
                return Ram[Ptr + index];

            return Ram[Ram[Ptr + index]];
        }
    }
}
