using EARIN_EX5.Helpers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX5.Entities {
    class BayesianNetwork {

        public BayesianNetwork(JObject jNetwork) {
            Nodes = jNetwork["nodes"].Value<JArray>().Select(node => new Node(node.Value<string>(), this)).ToList();

            var relations = jNetwork["relations"].Value<JObject>();

            foreach (var relation in relations) {
                var node = Nodes.Single(n => n.Name == relation.Key);
                node.InitParents(relation.Value["parents"].Value<JArray>().Select(obj => obj.Value<string>()).ToArray());
            }

            Nodes.ForEach(node => node.InitChildren());
            Nodes.ForEach(node => node.InitMarkovBlanket());

            foreach (var relation in relations) {
                var node = Nodes.Single(n => n.Name == relation.Key);
                var dict = new Dictionary<string, double>();

                var probabilities = relation.Value["probabilities"].Value<JObject>();

                foreach (var prob in probabilities) {
                    dict.Add(prob.Key, prob.Value.Value<double>());
                }

                node.InitProbabilities(dict);
            }
        }

        public List<Node> Nodes { get; private set; }

        public void SetEvidence(JObject evidence, out List<Node> evidenceNodes) {
            evidenceNodes = new List<Node>();
            foreach (var jNode in evidence) {
                var node = Nodes.Single(n => n.Name == jNode.Key);
                var value = jNode.Value.Value<string>();

                if (!node.PossibleValues.Contains(value)) {
                    ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.ValidationError, $"Setting evidence failure: node {node.Name} cannot be set to value {value}. Possible values are: {string.Join(", ", node.PossibleValues)}");
                }

                node.Value = value;
                evidenceNodes.Add(node);
            }
        }
    }
}
