using EARIN_EX5.Entities;
using System.Collections.Generic;
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
            /*
                        sb.AppendLine($"Dimensions: {Dimensions}");
                        sb.AppendLine($"d: {D}");
                        sb.AppendLine($"A");
                        sb.AppendLine($"{A}");
                        sb.AppendLine($"B: {B}");
                        sb.AppendLine($"C: {C}");

                        sb.AppendLine($"Crossover probability: {CrossoverProb}");
                        sb.AppendLine($"Mutation probability: {MutationProb}");
                        sb.AppendLine($"Iterations: {Iterations}");*/

            return sb.ToString();
        }
    }
}
