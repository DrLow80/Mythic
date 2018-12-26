using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Mythic.Feature.Engine.Tests
{
    [TestFixture]
    public class DiceRollTests
    {
        [TestCase(1, true)]
        [TestCase(100, true)]
        [TestCase(50, true)]
        [TestCase(int.MinValue, false)]
        [TestCase(int.MaxValue, false)]
        public void BuildTests(int value, bool isSuccess)
        {
            var result = DiceRoll.Build(value);

            Assert.AreEqual(result.IsSuccess, isSuccess);
        }

        [TestCase(11, 1)]
        [TestCase(22, 2)]
        [TestCase(33, 3)]
        [TestCase(44, 4)]
        [TestCase(55, 5)]
        [TestCase(66, 6)]
        [TestCase(77, 7)]
        [TestCase(88, 8)]
        [TestCase(99, 9)]
        [TestCase(100, int.MaxValue)]
        [TestCase(1, int.MaxValue)]
        [TestCase(10, int.MaxValue)]
        [TestCase(50, int.MaxValue)]
        public void ChaosTest(int value, int chaosValue)
        {
            var result = DiceRoll.Build(value);

            Assert.AreEqual(chaosValue, result.Value.Chaos);
        }

    }
}
