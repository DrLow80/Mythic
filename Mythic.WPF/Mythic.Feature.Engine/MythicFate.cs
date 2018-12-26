using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Mythic.Feature.Engine
{
    public class MythicFate
    {
        public const string InvalidChaos = "InvalidChaos";

        private static readonly int[] ChaosValues = { 11, 22, 33, 44, 55, 66, 77, 88, 99 };

        private MythicFate(int chaos, DiceRoll diceRoll, FateResult[] fateResults, bool hasRandomMythicEvent)
        {
            Chaos = chaos;
            DiceRoll = diceRoll;
            FateResults = fateResults;
            HasRandomMythicEvent = hasRandomMythicEvent;
        }

        public int Chaos { get; }

        public DiceRoll DiceRoll { get; }

        public FateResult[] FateResults { get; }

        public bool HasRandomMythicEvent { get; }

        public static Result<MythicFate> Build(int chaos, DiceRoll diceRoll = null)
        {
            if (chaos < 1 || chaos > 9) return Result.Fail<MythicFate>(InvalidChaos);

            diceRoll = diceRoll ?? DiceRoll.Rolld100();

            var fateResults = GetFateResults(chaos, diceRoll);

            var listFateResult = new List<FateResult>();

            foreach (var fateResult in fateResults)
            {
                if (fateResult.IsFailure) return Result.Fail<MythicFate>(fateResult.Error);

                listFateResult.Add(fateResult.Value);
            }

            var hasRandomEvent = CheckRollForRandomMythicEvent(chaos, diceRoll);

            var mythicFate = new MythicFate(chaos, diceRoll, listFateResult.ToArray(), hasRandomEvent);

            return Result.Ok(mythicFate);
        }

        private static bool CheckRollForRandomMythicEvent(int chaos, DiceRoll diceRoll)
        {
            return ChaosValues.Contains(diceRoll.Value) && diceRoll.Value >= chaos * 11;
        }

        private static IEnumerable<Result<FateResult>> GetFateResults(int chaos, DiceRoll diceRoll)
        {
            diceRoll = diceRoll ?? DiceRoll.Rolld100();

            return FateTable.GetMythicOddsByChaos(chaos).Select(mythicOdd => mythicOdd.Check(diceRoll));
        }
    }
}