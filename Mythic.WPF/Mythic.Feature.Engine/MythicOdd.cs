using CSharpFunctionalExtensions;

namespace Mythic.Feature.Engine
{
    public class MythicOdd
    {
        public const string ExceptionalFailureResult = "Exceptional Failure";
        public const string ExceptionalSuccessResult = "Exceptional Success";
        public const string FailureResult = "Failure";
        public const string NameNullOrEmpty = "NameNullOrEmpty";
        public const string SuccessResult = "Success";

        private MythicOdd(int index, string name, int exceptionalSuccess, int success, int failure)
        {
            Index = index;
            Name = name;
            ExceptionalSuccess = exceptionalSuccess;
            Success = success;
            Failure = failure;
        }

        public int ExceptionalSuccess { get; }
        public int Failure { get; }
        public int Index { get; }
        public string Name { get; }
        public int Success { get; }

        public static Result<MythicOdd> Build(int index, string name, int exceptionalSuccess, int success, int failure)
        {
            if (string.IsNullOrEmpty(name)) return Result.Fail<MythicOdd>(NameNullOrEmpty);

            var mythicOdd = new MythicOdd(index, name, exceptionalSuccess, success, failure);

            return Result.Ok(mythicOdd);
        }

        public static Result<MythicOdd> Build(int index, string name, int value)
        {
            int exceptionalSuccess = GetLowerTwentyPercent(value);

            int failure = GetUpperTwentyPercent(value);

            return Build(index, name, exceptionalSuccess, value, failure);
        }

        public Result<FateResult> Check(DiceRoll diceRoll)
        {
            var value = ToResult(diceRoll.Value);

            return FateResult.Build(Name, value);
        }

        public string ToResult(int value)
        {
            if (value <= ExceptionalSuccess)
            {
                return ExceptionalSuccessResult;
            }

            if (value <= Success)
            {
                return SuccessResult;
            }

            if (value <= Failure)
            {
                return FailureResult;
            }

            return ExceptionalFailureResult;
        }

        private static int GetLowerTwentyPercent(int value)
        {
            var result = (int)(value * .2);

            if (result < 1)
            {
                result = 0;
            }

            return result;
        }

        private static int GetUpperTwentyPercent(int value)
        {
            if (value > 100)
            {
                return 0;
            }

            var failure = 100 - value;

            failure = GetLowerTwentyPercent(failure);

            failure = 101 - failure;

            return failure;
        }
    }
}