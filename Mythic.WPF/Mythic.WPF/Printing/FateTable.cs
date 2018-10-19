namespace Mythic.WPF.Printing
{
    public class FateTable
    {
        private static readonly MythicOdd[] MythicOdds =
        {
            MythicOdd.Build(0, "Impossible", 1, 5, 82).Value,
            MythicOdd.Build(1, "No Way", 3, 15, 84).Value,
            MythicOdd.Build(2, "Very unlikely", 5, 25, 86).Value,
            MythicOdd.Build(3, "Unlikely", 7, 35, 88).Value,
            MythicOdd.Build(4, "50/50", 10, 50, 91).Value,
            MythicOdd.Build(5, "Somewhat likely", 13, 65, 94).Value,
            MythicOdd.Build(6, "Likely", 15, 75, 96).Value,
            MythicOdd.Build(7, "Very likely", 18, 85, 97).Value,
            MythicOdd.Build(8, "Near sure thing", 18, 90, 99).Value,
            MythicOdd.Build(9, "A sure thing", 18, 90, 99).Value,
            MythicOdd.Build(10, "Has to be", 19, 95, 100).Value
        };

        public static MythicOdd[] GetMythicOddsByChaos(int chaos)
        {
            return MythicOdds;
        }
    }
}