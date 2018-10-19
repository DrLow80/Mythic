using CSharpFunctionalExtensions;

namespace Mythic.WPF.Printing
{
    public class FateResult
    {
        public const string NameInvalid = "NameInvalid";
        public const string ValueInvalid = "ValueInvalid";

        private FateResult(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }

        public static Result<FateResult> Build(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) return Result.Fail<FateResult>(NameInvalid);

            if (string.IsNullOrEmpty(value)) return Result.Fail<FateResult>(ValueInvalid);

            var fateResult = new FateResult(name, value);

            return Result.Ok(fateResult);
        }
    }
}