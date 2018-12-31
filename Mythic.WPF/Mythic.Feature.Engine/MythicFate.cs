using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Mythic.Feature.Engine
{
    public class MythicFate
    {
        public const string InvalidChaos = "InvalidChaos";

        private MythicFate(Chaos chaos, DiceRoll diceRoll, IEnumerable<FateResult> fateResults)
        {
            Chaos = chaos;
            DiceRoll = diceRoll;
            FateResults = fateResults;
        }

        public Chaos Chaos { get; }

        public DiceRoll DiceRoll { get; }

        public IEnumerable<FateResult> FateResults { get; }

        public bool HasRandomMythicEvent => DiceRoll.Chaos <= Chaos;

        public static Result<MythicFate> Build(Chaos chaos, DiceRoll diceRoll = null, params string[] odds)
        {
            diceRoll = diceRoll ?? DiceRoll.Rolld100().Value;

            var fateResults = FateTable.GetFateResults(chaos, diceRoll, odds);

            var mythicFate = new MythicFate(chaos, diceRoll, fateResults);

            return Result.Ok(mythicFate);
        }
    }
}