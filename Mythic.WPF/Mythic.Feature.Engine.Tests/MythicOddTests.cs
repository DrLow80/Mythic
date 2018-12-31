using System;
using NUnit.Framework;

namespace Mythic.Feature.Engine.Tests
{
    [TestFixture]
    public class MythicOddTests
    {
        [TestCase(50, 10, 91)]
        [TestCase(0, 0, 81)]
        [TestCase(5, 1, 82)]
        [TestCase(10, 2, 83)]
        [TestCase(-20, 0, 77)]
        [TestCase(145, 29, 0)]
        public void BuildTests(int value, int exceptionalSuccess, int failure)
        {
            var result = MythicOdd.Build(0, Guid.NewGuid().ToString(), value);

            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(failure, result.Value.Failure);

            Assert.AreEqual(exceptionalSuccess, result.Value.ExceptionalSuccess);
        }

        [TestCase(1, 5, 82)]
        [TestCase(2, 10, 83)]
        [TestCase(10, 50, 91)]
        [TestCase(16, 85, 98)]
        [TestCase(18, 90, 99)]
        public void ToResultTests(int exceptionalSuccess, int success, int failure)
        {
            var result = MythicOdd.Build(0, Guid.NewGuid().ToString(), success);

            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(MythicOdd.ExceptionalSuccessResult, result.Value.ToResult(1));

            Assert.AreEqual(MythicOdd.ExceptionalSuccessResult, result.Value.ToResult(exceptionalSuccess));

            Assert.AreEqual(MythicOdd.SuccessResult, result.Value.ToResult(success));

            Assert.AreEqual(MythicOdd.FailureResult, result.Value.ToResult(failure));

            Assert.AreEqual(MythicOdd.ExceptionalFailureResult, result.Value.ToResult(failure + 1));

            Assert.AreEqual(MythicOdd.ExceptionalFailureResult, result.Value.ToResult(100));
        }
    }
}