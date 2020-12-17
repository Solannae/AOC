namespace AdventOfCode_2020.Libs
{
    public class Adapter
    {
        public int Joltage { get; set; }
        public Adapter connectedAdapter { get; set; }

        public Adapter(int power)
        {
            this.Joltage = power;
        }
    }
}