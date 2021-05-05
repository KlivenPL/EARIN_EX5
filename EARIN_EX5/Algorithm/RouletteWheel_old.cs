using System;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX5.Algorithm {
    public class RouletteWheel_old<T> {

        private readonly List<(T item, double prob)> itemsWithProbs;
        private static readonly Random random = new Random();

        public RouletteWheel_old(ICollection<T> collection, ICollection<double> probabilities, bool normalizeProbabilities = false) {
            var normalizedProbabilities = normalizeProbabilities ? ProbabilityNormalizer.Normalize(probabilities) : probabilities;
            itemsWithProbs = collection.Select((c, i) => (c, normalizedProbabilities.ElementAt(i))).ToList();
        }

        public T Spin() {
            double max = 0;
            int maxI = 0;
            for (int i = 0; i < itemsWithProbs.Count; i++) {
                if (itemsWithProbs[i].prob > max) {
                    max = itemsWithProbs[i].prob;
                    maxI = i;
                }
            }
            return itemsWithProbs[maxI].item;
            /*var rand = random.NextDouble();
            double currentVal = 0;
            int i = 0;

            while (currentVal < rand) {
                currentVal += itemsWithProbs[i].prob;

                if (i >= itemsWithProbs.Count - 1)
                    break;
                i++;
            }

            return itemsWithProbs[i].item;*/
        }
    }
}
