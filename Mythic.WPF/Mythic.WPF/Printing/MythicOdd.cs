using CSharpFunctionalExtensions;

namespace Mythic.WPF.Printing
{
    public class MythicOdd
    {
        public const string ExceptionalSuccessInvalid = "ExceptionalSuccessInvalid";
        public const string FailureInvalid = "FailureInvalid";
        public const string NameNullOrEmpty = "NameNullOrEmpty";
        public const string SuccessInvalid = "SuccessInvalid";

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
        public string Name { get; }
        public int Success { get; }
        public int Index { get; }

        public static Result<MythicOdd> Build(int index, string name, int exceptionalSuccess, int success, int failure)
        {
            if (string.IsNullOrEmpty(name)) return Result.Fail<MythicOdd>(NameNullOrEmpty);

            if (exceptionalSuccess < 1 || exceptionalSuccess > success || exceptionalSuccess > failure)
                return Result.Fail<MythicOdd>(ExceptionalSuccessInvalid);

            if (success < 1 || success < exceptionalSuccess || success > failure)
                return Result.Fail<MythicOdd>(SuccessInvalid);

            if (failure < 1 || failure < exceptionalSuccess || failure < success)
                return Result.Fail<MythicOdd>(FailureInvalid);

            var mythicOdd = new MythicOdd(index, name, exceptionalSuccess, success, failure);

            return Result.Ok(mythicOdd);
        }

        public Result<FateResult> Check(DiceRoll diceRoll)
        {
            var value = ToResult(diceRoll);

            return FateResult.Build(Name, value);
        }

        private string ToResult(DiceRoll diceRoll)
        {
            if (diceRoll.Value > Failure)
            {
                return "Exceptional Failure";
            }

            if (diceRoll.Value > Success)
            {
                return "Failure";
            }

            if (diceRoll.Value > ExceptionalSuccess)
            {
                return "Success";
            }

            return "Exceptional Success";
        }
    }
}