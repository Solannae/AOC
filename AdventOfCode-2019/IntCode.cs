using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AdventOfCode
{
    class IntCode
    {
        private readonly long[] Rom;
        public long[] Ram { get; private set; }
        public int Ptr { get; private set; }
        public int Relative { get; private set; }
        public long Register { get; private set; }
        public Queue<long> Buffer { get; private set; }
        public bool IsRunning { get; private set; }

        public IntCode(long[] array)
        {
            this.Ram = (long[])array.Clone();
            this.Rom = (long[])array.Clone();
            this.Ptr = 0;
            this.Relative = 0;
            this.Register = 0;
            this.Buffer = new Queue<long>();
            this.IsRunning = false;
        }

        public void Reset()
        {
            Ram = (long[])Rom.ToArray().Clone();
            Buffer.Clear();
            Ptr = 0;
            Register = 0;
            IsRunning = false;
        }

        public void RunIntcode()
        {
            IsRunning = true;

            while (Ram[Ptr] != 99)
            {
                var opcode = Ram[Ptr] % 100;

                switch (opcode)
                {
                    // Add
                    case 1:
                        while (Ram[Ptr + 3] >= Ram.Length) ResizeMemory();
                        Ram[Ram[Ptr + 3]] = GetParameter(1) + GetParameter(2);
                        Ptr += 4;
                        break;

                    // Multiply
                    case 2:
                        while (Ram[Ptr + 3] >= Ram.Length) ResizeMemory();
                        Ram[Ram[Ptr + 3]] = GetParameter(1) * GetParameter(2);
                        Ptr += 4;
                        break;

                    // ReadInput
                    case 3:
                        if (Buffer.Count > 0)
                        {
                            InjectInput(Buffer.Dequeue());
                        }
                        else
                        {
                            return;
                        }
                        break;

                    // WriteOutput
                    case 4:
                        Register = GetParameter(1);
                        Console.WriteLine(Register);
                        Ptr += 2;
                        break;

                    // JumpIfTrue
                    case 5:
                        if (GetParameter(1) != 0)
                            Ptr = (int)GetParameter(2);
                        else
                            Ptr += 3;
                        break;

                    // JumpIfFalse
                    case 6:
                        if (GetParameter(1) == 0)
                            Ptr = (int)GetParameter(2);
                        else
                            Ptr += 3;
                        break;

                    // LessThan
                    case 7:
                        while (Ram[Ptr + 3] >= Ram.Length) ResizeMemory();
                        if (GetParameter(1) < GetParameter(2))
                            Ram[Ram[Ptr + 3]] = 1;
                        else
                            Ram[Ram[Ptr + 3]] = 0;
                        Ptr += 4;
                        break;

                    // Equal
                    case 8:
                        while (Ram[Ptr + 3] >= Ram.Length) ResizeMemory();
                        if (GetParameter(1) == GetParameter(2))
                            Ram[Ram[Ptr + 3]] = 1;
                        else
                            Ram[Ram[Ptr + 3]] = 0;
                        Ptr += 4;
                        break;

                    // Set Relative
                    case 9:
                        Relative += (int)GetParameter(1);
                        Ptr += 2;
                        break;

                    default:
                        return;
                }

                if (Ptr >= Ram.Length)
                    ResizeMemory();
            }

            IsRunning = false;
        }

        public long GetParameter(int index)
        {
            var debug = Math.Pow(10, index - 1);
            var isImmediate = (int)(Ram[Ptr] % (1000 * debug) / (100 * debug)) == 1;
            var isRelative = (int)(Ram[Ptr] % (1000 * debug) / (100 * debug)) == 2;

            if (isRelative)
            {
                while (Relative + Ram[Ptr + index] >= Ram.Length) ResizeMemory();
                return Ram[Relative + Ram[Ptr + index]];
            }

            if (isImmediate)
                return Ram[Ptr + index];

            while (Ram[Ptr + index] >= Ram.Length) ResizeMemory();
            return Ram[Ram[Ptr + index]];
        }

        public void PushToBuffer(long input)
        {
            Buffer.Enqueue(input);
        }

        private void InjectInput(long input)
        {
            var isRelative = (int)(Ram[Ptr] % 1000 / 100) == 2;
            if (isRelative)
            {
                while (Relative + Ram[Ptr + 1] >= Ram.Length) ResizeMemory();
                Ram[Relative + Ram[Ptr + 1]] = input;
            }
            else
            {
                while (Ram[Ptr + 1] >= Ram.Length) ResizeMemory();
                Ram[Ram[Ptr + 1]] = input;
            }

            Ptr += 2;
        }

        private void ResizeMemory()
        {
            var tmp = Ram;
            Array.Resize(ref tmp, tmp.Length * 2);
            Ram = tmp;
        }
    }
}
