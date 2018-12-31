using System;
using System.Collections.Generic;
using System.Linq;

namespace Mythic.Feature.Engine
{
    public class FateTable
    {
        public static string[] FateOdds =
        {
            "Impossible", "No Way", "Very unlikely", "Unlikely", "50/50", "Somewhat likely", "Likely", "Very likely",
            "Near sure thing", "A sure thing", "Has to be"
        };

        private static readonly IDictionary<int, IEnumerable<MythicOdd>> Dictionary =
            new Dictionary<int, IEnumerable<MythicOdd>>
            {
                {9, Build(50, 75, 85, 90, 95, 95, 100, 105, 115, 125, 145)},
                {8, Build(25, 50, 65, 75, 85, 90, 95, 95, 100, 110, 130)},
                {7, Build(15, 35, 50, 55, 75, 85, 90, 95, 95, 95, 100)},
                {6, Build(10, 25, 45, 50, 65, 80, 85, 90, 95, 95, 100)},
                {5, Build(5, 15, 25, 35, 50, 65, 75, 85, 90, 90, 95)},
                {4, Build(5, 10, 15, 20, 35, 50, 55, 75, 80, 85, 95)},
                {3, Build(0, 5, 10, 15, 25, 45, 50, 65, 75, 80, 90)},
                {2, Build(0, 5, 5, 10, 15, 25, 35, 50, 55, 65, 85)},
                {1, Build(-20, 0, 5, 5, 10, 20, 25, 45, 50, 55, 80)}
            };

        private static IEnumerable<MythicOdd> Build(params int[] values)
        {
            if (values.Length < FateOdds.Length) throw new ApplicationException();

            for (var i = 0; i < FateOdds.Length; i++)
            {
                var result = MythicOdd.Build(i, FateOdds.ElementAt(i), values.ElementAt(i));

                if (result.IsFailure) throw new ApplicationException(result.Error);

                yield return result.Value;
            }
        }

        public static IEnumerable<FateResult> GetFateResults(Chaos chaos, DiceRoll diceRoll, string[] odds)
        {
            var mythicOdds = Dictionary[chaos].ToDictionary(x => x.Name);

            if (!odds.Any()) odds = FateOdds;

            foreach (var odd in odds)
            {
                var result = mythicOdds[odd].Check(diceRoll);

                if (result.IsFailure) throw new ApplicationException(result.Error);

                yield return result.Value;
            }
        }
    }
}