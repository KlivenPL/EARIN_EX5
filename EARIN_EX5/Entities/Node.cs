using EARIN_EX5.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX5.Entities {
    class Node {
        private readonly BayesianNetwork network;

        public Node(string name, BayesianNetwork network) {
            this.network = network;
            Name = name;
        }

        public string Name { get; }
        public string Value { get; set; }
        public List<Node> Parents { get; private set; }
        public List<Node> Children { get; private set; }
        public List<Probability> Probabilities { get; private set; }
        public List<Node> MarkovBlanket { get; private set; }
        public List<string> PossibleValues { get; private set; }

        public void InitParents(string[] parents) {
            Parents = parents.Select(p => network.Nodes.Single(node => node.Name == p)).ToList();
        }

        public void InitChildren() {
            Children = network.Nodes.Where(n => n.Parents.Contains(this)).ToList();
        }

        public void InitMarkovBlanket() {
            MarkovBlanket = Parents.Concat(Children)
                .Concat(Children.SelectMany(c => c.Parents))
                .Distinct()
                .Where(n => n != this)
                .ToList();
        }

        public void InitProbabilities(Dictionary<string, double> probabilities) {
            Probabilities = new List<Probability>();

            var possibleValues = new List<string>();
            foreach (var prob in probabilities) {
                var splitedNames = prob.Key.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

                if (splitedNames.Length != Parents.Count + 1)
                    ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.ValidationError, $"Invalid probability: {prob.Key} - should contain {Parents.Count + 1} parameters.");

                possibleValues.Add(splitedNames.Last());

                var probability = new Probability(prob.Key, prob.Value);
                Probabilities.Add(probability);
            }

            PossibleValues = possibleValues.Distinct().ToList();
        }

        public override string ToString() {
            return $"{Name}: {Value}";
        }
    }
}
