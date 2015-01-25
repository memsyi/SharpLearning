using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SharpLearning.Classifier;

namespace SharpLearningTests.Classifier
{
    [TestFixture]
    public class KNearestNeighbourTests
    {
        [Test]
        public void IfNotTrainedDefaultsToTrue()
        {
            // ARRANGE
            var classifier = CreateKNearestNeighbourClassifier<string>(1, (a, b) => 0);
            classifier.Train(new Dictionary<string, bool>());
            const string input = "abc";

            // ACT
            var result = classifier.Classify(input);

            // ASSERT
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IfGivenATrainedPointWithKEqualOneGivesSameResult(bool value)
        {
            // ARRANGE
            // If same item returns 0, otherwise return 1
            Func<string, string, int> distanceCalculator = (a, b) => a == b ? 0 : 1;
            var classifier = CreateKNearestNeighbourClassifier(1, distanceCalculator);
            classifier.Train(new Dictionary<string, bool>
            {
                {"abc", value},
                {"def", !value},
                {"ghi", !value},
                {"jkl", !value}
            });
            const string input = "abc";

            // ACT
            var result = classifier.Classify(input);

            // ASSERT
            Assert.That(result, Is.EqualTo(value));
        }

        private static KNearestNeighbourClassifier<T> CreateKNearestNeighbourClassifier<T>(int k, Func<T, T, int> distanceCalculator)
        {
            return new KNearestNeighbourClassifier<T>(k, distanceCalculator);
        }
    }
}