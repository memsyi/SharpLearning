using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpLearning.Classifier
{
    /// <summary>
    /// Classifies an item using the k nearest neighbour algorithm.
    /// See http://en.wikipedia.org/wiki/K-nearest_neighbors_algorithm.
    /// </summary>
    public class KNearestNeighbourClassifier<T> : IBooleanClassifier<T>
    {
        private readonly IDictionary<T, bool> _trainingSet = new Dictionary<T, bool>();
        private readonly int _k;
        private readonly Func<T, T, int> _distanceFunction;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="k">The number of nearest neighbours to consider.</param>
        /// <param name="distanceFunction">Calculate the distance between two items.</param>
        public KNearestNeighbourClassifier(int k, Func<T, T, int> distanceFunction)
        {
            _k = k;
            _distanceFunction = distanceFunction;
        }

        public void Train(IDictionary<T, bool> trainingSet)
        {
            foreach (var trainingItem in trainingSet)
            {
                _trainingSet.Add(trainingItem);
            }
        }

        public bool Classify(T item)
        {
            var k = Math.Min(_k, _trainingSet.Count);
            var kNearest = _trainingSet.OrderBy(kvp => _distanceFunction(kvp.Key, item)).Take(k);
            var numTrue = kNearest.Select(kvp => kvp.Value).Count(value => value);
            return 2*numTrue >= k;
        }
    }
}