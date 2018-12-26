using System;
using CSharpFunctionalExtensions;

namespace Mythic.Feature.Engine
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

            if (exceptionalSuccess < 0 || exceptionalSuccess > success || exceptionalSuccess > failure)
                return Result.Fail<MythicOdd>($"{ExceptionalSuccessInvalid} - exceptionalSuccess: {exceptionalSuccess} success: {success} failure: {failure}");

            if (success < 0 || success < exceptionalSuccess || success > failure)
                return Result.Fail<MythicOdd>($"{SuccessInvalid} - exceptionalSuccess: {exceptionalSuccess} success: {success} failure: {failure}");

            if (failure < 1 || failure < exceptionalSuccess || failure < success)
                return Result.Fail<MythicOdd>($"{FailureInvalid} - exceptionalSuccess: {exceptionalSuccess} success: {success} failure: {failure}");

            var mythicOdd = new MythicOdd(index, name, exceptionalSuccess, success, failure);

            return Result.Ok(mythicOdd);
        }

        public static Result<MythicOdd> Build(int index, string name, int value)
        {
            int exceptionalSuccess = (int)Math.Floor(value * .2);

            int failure = 0;//(int) (100 - (((100 - value) * .2) - 1));

            if (value < 1)
            {
                value = 0;

                exceptionalSuccess = 0;

                failure = 77;
            }
            else
            {
                failure = 100 - value;

                failure = (int) (failure * .2);

                failure--;

                failure = 100 - failure;
            }

            if (value>100)
            {
                failure = value + 1;
            }

            return Build(index, name, exceptionalSuccess, value, failure);
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