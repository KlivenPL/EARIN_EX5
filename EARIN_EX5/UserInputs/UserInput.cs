using EARIN_EX5.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EARIN_EX5.UserInputs {
    class UserInput {
        public BayesianNetwork Network { get; set; }
        public Node MarkovBlanketNode { get; set; }
        public List<Node> QueryNodes { get; set; }
        public List<Node> EvidenceNodes { get; set; }
        public int Steps { get; set; }

        public override string ToString() {
            var sb = new StringBuilder();

            sb.AppendLine($"\tSelected markov blanket node: {MarkovBlanketNode.Name ?? "(none)"}");
            sb.AppendLine($"\tQuery nodes: {string.Join(", ", QueryNodes.Select(n => n.Name))}");
            sb.AppendLine($"\tEvidence nodes: {string.Join(", ", EvidenceNodes.Select(n => n.ToString()))}");
            sb.AppendLine($"\tSteps: {Steps}");

            return sb.ToString();
        }
    }
}
