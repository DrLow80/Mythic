using CSharpFunctionalExtensions;

namespace Mythic.Feature.Engine
{
    public class Chaos
    {
        private Chaos(int value = Default)
        {
            Value = value;
        }

        public const string InvalidChaos = "InvalidChaos";

        public int Value { get; private set; }

        public const int Min = 1;
        public const int Max = 9;
        public const int Default = 5;

        public static Result<Chaos> Build(int value)
        {
            if (value < Min || value > Max) return Result.Fail<Chaos>(InvalidChaos);

            Chaos chaos = new Chaos(value);

            return Result.Ok(chaos);
        }

        public static implicit operator int(Chaos chaos)
        {
            return chaos?.Value ?? Default;
        }

        public static implicit operator Chaos(int value)
        {
            var result = Build(value);

            return result.IsFailure ? new Chaos() : result.Value;
        }

        public Chaos Increment()
        {
            if (Value == Max)
            {
                return this;
            }

            return ++Value;
        }

        public Chaos Decrement()
        {
            if (Value == Min)
            {
                return this;
            }

            return --Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}