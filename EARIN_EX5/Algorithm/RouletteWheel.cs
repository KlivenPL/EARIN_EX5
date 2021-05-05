using System;
using System.Collections.Generic;

namespace EARIN_EX5.Algorithm {
    class RouletteWheel {

        private readonly List<double> probabilities;
        private readonly Random random;

        public RouletteWheel(List<double> probabilities) {
            this.probabilities = probabilities;
            random = new Random();
        }

        public int Spin() {
            var rand = random.NextDouble();
            double currentVal = 0;
            int i = 0;

            while (currentVal < rand) {
                currentVal += probabilities[i];

                if (currentVal >= rand)
                    break;
                i++;
            }

            return i;
        }
    }
}
