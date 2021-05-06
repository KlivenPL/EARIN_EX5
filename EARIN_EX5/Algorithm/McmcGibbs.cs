using EARIN_EX5.Entities;
using EARIN_EX5.Helpers;
using EARIN_EX5.UserInputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX5.Algorithm {
    class McmcGibbs {
        private readonly UserInput input;
        private readonly List<Node> nonEvidenceNodes;

        public McmcGibbs(UserInput input) {
            this.input = input;
            nonEvidenceNodes = input.Network.Nodes.Where(n => input.EvidenceNodes?.Contains(n) != true).ToList();
        }

        public void EvaluateQueries() {
            // Observed values for evidence nodes are already set - we randomly draw values for non-evidence nodes.
            nonEvidenceNodes.ForEach(n => n.Value = n.PossibleValues.PickRandom());

            var counter = new Dictionary<Node, Dictionary<string, double>>();

            foreach (var queryNode in input.QueryNodes) {
                counter.Add(queryNode, new Dictionary<string, double>());
                queryNode.PossibleValues.ForEach(possibleValue => counter[queryNode].Add(possibleValue, 0));
            }

            for (int i = 0; i < input.Steps; i++) {
                var node = nonEvidenceNodes.PickRandom();
                node.Value = SampleUsingMarkovBlanket(node);

                foreach (var queryNode in input.QueryNodes) {
                    counter[queryNode][queryNode.Value] += 1.0 / input.Steps;
                }
            }

            foreach (var result in counter) {
                Console.WriteLine($"Query for {result.Key.Name}:");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var subResult in result.Value) {
                    Console.WriteLine($"\t{subResult.Key}: {subResult.Value}");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private string SampleUsingMarkovBlanket(Node node) {
            var probabilities = new List<(string name, double prob)>();
            var normalizedProbabilities = new List<(string name, double prob)>();

            foreach (var xj in node.PossibleValues) {
                node.Value = xj;

                var currentNodeState = string.Join(",", node.Parents.Select(p => p.Value).Concat(new[] { xj }).ToArray());
                var p = node.Probabilities.Single(p => p.ConcatedNames == currentNodeState).Value;

                var pRest = 1.0;

                foreach (var child in node.Children) {
                    var currentChildState = string.Join(",", child.Parents.Select(p => p.Value).Concat(new[] { child.Value }).ToArray());
                    var pChild = child.Probabilities.Single(p => p.ConcatedNames == currentChildState).Value;

                    pRest *= pChild;
                }

                p *= pRest;
                probabilities.Add((xj, p));
            }

            var sum = probabilities.Sum(p => p.prob);
            foreach (var prob in probabilities) {
                normalizedProbabilities.Add((prob.name, prob.prob / sum));
            }

            return normalizedProbabilities.RoulettePick(x => x.prob).name;
        }
    }
}
