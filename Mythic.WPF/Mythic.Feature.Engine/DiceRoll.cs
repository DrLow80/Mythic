using System;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;

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

        public static Result<DiceRoll> Rolld10()
        {
            var value = Random.Next(Min, Ten);

            return Build(value);
        }

        public static Result<DiceRoll> Rolld100()
        {
            var value = Random.Next(Min, Max);

            return Build(value);
        }

        public const string FailInvalidValue = "FailInvalidValue";

        public const int Max = 100;
        public const int Min = 1;
        public const int Doubles = 11;
        public const int Ten = 10;


        public static Result<DiceRoll> Build(int value)
        {
            if (value < Min || value > Max)
            {
                return Result.Fail<DiceRoll>(FailInvalidValue);
            }

            DiceRoll diceRoll = new DiceRoll(value);

            return Result.Ok(diceRoll);
        }

        public bool IsDoubles => Value % Doubles == 0;

        public int Chaos => IsDoubles ? Value / Doubles : Ten;

        public static implicit operator int(DiceRoll diceRoll)
        {
            return diceRoll?.Value ?? Min;
        }

        public static implicit operator DiceRoll(int value)
        {
            var result = Build(value);

            return result.IsFailure ? new DiceRoll(Min) : result.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}