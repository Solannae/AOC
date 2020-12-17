namespace AdventOfCode_2020.Libs
{
    public class Instruction
    {
        public string Name;
        public int Value;
        public bool Executed;

        public Instruction(string name, int value)
        {
            Name = name;
            Value = value;
            Executed = false;
        }

        public static Instruction ParseInstruction(string line)
        {
            var split = line.Split(" ");
            return new Instruction(split[0], int.Parse(split[1]));
        }
    }
}
