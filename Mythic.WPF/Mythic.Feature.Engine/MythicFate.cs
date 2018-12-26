using CSharpFunctionalExtensions;
using System.Collections.Generic;

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
            diceRoll = diceRoll ?? Engine.DiceRoll.Rolld100().Value;

            IEnumerable<FateResult> fateResults = FateTable.GetFateResults(chaos, diceRoll, odds);

            MythicFate mythicFate = new MythicFate(chaos, diceRoll, fateResults);

            return Result.Ok(mythicFate);
        }
    }
}