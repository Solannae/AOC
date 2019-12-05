using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class IntCode
    {
        public void RunIntcode(int[] array)
        {
            var index = 0;

            while (array[index] != 99)
            {
                var opcode = array[index] % 100;
                var immediate1st = (array[index] % 1000 / 100 == 1);
                var immediate2nd = (array[index] % 10000 / 1000 == 1);
                var immediate3rd = (array[index] % 100000 / 10000 == 1);

                switch (opcode)
                {
                    // Add
                    case 1:
                        array[array[index + 3]] = (immediate1st ? array[index + 1] : array[array[index + 1]])
                                                + (immediate2nd ? array[index + 2] : array[array[index + 2]]);
                        index += 4;
                        break;

                    // Multiply
                    case 2:
                        array[array[index + 3]] = (immediate1st ? array[index + 1] : array[array[index + 1]])
                                                * (immediate2nd ? array[index + 2] : array[array[index + 2]]);
                        index += 4;
                        break;

                    // ReadInput
                    case 3:
                        var line = Console.ReadLine();
                        if (int.TryParse(line, out var nb))
                            array[array[index + 1]] = nb;
                        index += 2;
                        break;

                    // WriteOutput
                    case 4:
                        Console.WriteLine(immediate1st ? array[index + 1] : array[array[index + 1]]);
                        index += 2;
                        break;

                    // JumpIfTrue
                    case 5:
                        if ((immediate1st ? array[index + 1] : array[array[index + 1]]) != 0)
                            index = (immediate2nd ? array[index + 2] : array[array[index + 2]]);
                        else
                            index += 3;
                        break;

                    // JumpIfFalse
                    case 6:
                        if ((immediate1st ? array[index + 1] : array[array[index + 1]]) == 0)
                            index = (immediate2nd ? array[index + 2] : array[array[index + 2]]);
                        else
                            index += 3;
                        break;

                    // LessThan
                    case 7:
                        if ((immediate1st ? array[index + 1] : array[array[index + 1]]) < (immediate2nd ? array[index + 2] : array[array[index + 2]]))
                            array[array[index + 3]] = 1;
                        else
                            array[array[index + 3]] = 0;
                        index += 4;
                        break;

                    // Equal
                    case 8:
                        if ((immediate1st ? array[index + 1] : array[array[index + 1]]) == (immediate2nd ? array[index + 2] : array[array[index + 2]]))
                            array[array[index + 3]] = 1;
                        else
                            array[array[index + 3]] = 0;
                        index += 4;
                        break;

                    default:
                        return;
                }
            }
        }
    }
}
