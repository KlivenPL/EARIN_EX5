using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX5.Algorithm {
    public static class ProbabilityNormalizer {
        public static double[] Normalize(ICollection<double> valuesCol) {
            var sum = valuesCol.Sum();
            var probs = valuesCol.Select(val => val / sum).ToArray();
            return probs;
        }
    }
}
