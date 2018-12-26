﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Mythic.Feature.Engine
{
    public class FateTable
    {
        public static string[] FateOdds = { "Impossible", "No Way", "Very unlikely", "Unlikely", "50/50", "Somewhat likely", "Likely", "Very likely", "Near sure thing", "A sure thing", "Has to be" };

        private static readonly IDictionary<int, IEnumerable<MythicOdd>> Dictionary = new Dictionary<int, IEnumerable<MythicOdd>>()
        {
            { 1, Build(50,75,85,90,95,95,100,105,115,125,145)},
            { 2, Build(25,50,65,75,85,90,95,95,100,110,130)},
            { 3, Build(15,35,50,55,75,85,90,95,95,95,100)},
            { 4, Build(10,25,45,50,65,80,85,90,95,95,100)},
            { 5, Build(5,15,25,35,50,65,75,85,90,90,95)},
            { 6, Build(5,10,15,20,35,50,55,75,80,85,95)},
            { 7, Build(0,5,10,15,25,45,50,65,75,80,90)},
            { 8, Build(0,5,5,10,15,25,35,50,55,65,85)},
            { 9, Build(-20,0,5,5,10,20,25,45,50,55,80)},
        };

        public static MythicOdd[] GetMythicOddsByChaos(int chaos)
        {
            return Dictionary[chaos].ToArray();
        }

        public static MythicOdd GetMythicOdd(int chaos, string odd)
        {
            var result = GetMythicOddsByChaos(chaos);

            return result.First(x => x.Name == odd);
        }

        private static IEnumerable<MythicOdd> Build(params int[] values)
        {
            if (values.Length < 11)
            {
                throw new ApplicationException();
            }

            for (int i = 0; i < 11; i++)
            {
                var result = MythicOdd.Build(i, FateOdds.ElementAt(i), values.ElementAt(i));

                if (result.IsFailure)
                {
                    throw new ApplicationException(result.Error);
                }

                yield return result.Value;
            }
        }
    }
}