using System.Collections.Generic;

namespace SharpLearning.Classifier
{
    /// <summary>
    /// Classifies an item of type <typeparamref name="T"/> as a boolean.
    /// </summary>
    public interface IBooleanClassifier<T>
    {
        /// <summary>
        /// Trains the classifier on the given <paramref name="trainingSet"/>.
        /// </summary>
        /// <param name="trainingSet">The map from inputs to outputs to train on.</param>
        void Train(IDictionary<T, bool> trainingSet);

        /// <summary>
        /// Classify the given <paramref name="item"/> as a boolean.
        /// </summary>
        bool Classify(T item);
    }
}