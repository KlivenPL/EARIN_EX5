using EARIN_EX5.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX5.UserInputs {
    class UserInputValidator {
        private readonly UserInput userInput;

        public UserInputValidator(UserInput userInput) {
            this.userInput = userInput;
        }

        public void Validate() {
            ValidateIfAcyclic();
            ValidateProbabilities();
            ValidateProbabilitiesSums();
            ValidateEvidenceAndQuery();
        }

        private void ValidateEvidenceAndQuery() {
            if (userInput.EvidenceNodes?.Any() == true && userInput.QueryNodes?.Any() != true) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.InvalidInput, "If evidence is defined, program expects queries to be defined as well");
            }
        }

        private void ValidateIfAcyclic() {
            var network = userInput.Network.DeepCopy();

            var S = network.Nodes.Where(n => !n.Parents.Any()).ToList();

            while (S.Any()) {
                var n = S.First();
                S.Remove(n);

                foreach (var m in network.Nodes.Where(m => n.Children.Contains(m))) {
                    n.Children.Remove(m);
                    m.Parents.Remove(n);
                    if (!m.Parents.Any()) {
                        S.Add(m);
                    }
                }
            }

            if (network.Nodes.Any(n => n.Children.Any() || n.Parents.Any())) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.ValidationError, "Given network contains at least one cycle");
            }
        }

        private void ValidateProbabilities() {
            var errors = userInput.Network.Nodes
                .Where(n => !n.Probabilities.Any())
                .Select(n => $"Node {n.Name} does not have probability table defined");

            if (errors.Any()) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.ValidationError, errors.ToArray());
            }
        }

        private void ValidateProbabilitiesSums() {
            var errors = new List<string>();
            foreach (var node in userInput.Network.Nodes) {
                var splitedProb = node.Probabilities
                    .Select(p => new {
                        Name = string.Join(",", p.ConcatedNames.Split(",", System.StringSplitOptions.RemoveEmptyEntries).SkipLast(1)),
                        Value = p.Value
                    });

                var groups = splitedProb
                    .GroupBy(sp => sp.Name)
                    .Select(g => new {
                        g.Key,
                        Value = g.Sum(s => s.Value)
                    });

                if (groups.Any(g => g.Value != 1)) {
                    errors.Add($"Incorrect probabilities for node {node.Name}");
                }
            }

            if (errors.Any()) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.ValidationError, errors.ToArray());
            }
        }
    }
}
