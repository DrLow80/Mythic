using System;

namespace Mythic.Feature.Engine
{
    public class DiceRoll
    {
        private static readonly Random Random = new Random();

        private DiceRoll(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static DiceRoll Rolld10()
        {
            return new DiceRoll(Random.Next(1, 10));
        }

        public static DiceRoll Rolld100()
        {
            return new DiceRoll(Random.Next(1, 100));
        }
    }
}