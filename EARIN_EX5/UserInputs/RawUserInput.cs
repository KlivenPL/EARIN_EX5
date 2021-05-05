using CommandLine;
using EARIN_EX5.Entities;
using EARIN_EX5.Helpers;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace EARIN_EX5.UserInputs {
    class RawUserInput {
        [Option('p', "network-path", Required = true, HelpText = "Bayesian network JSON file path")]
        public string NetworkPath { get; set; }

        [Option('e', "evidence", Required = false, HelpText = "Evidence, given in JSON object format, eg. {\"burglary\": 1, \"alarm\": 1}. If defined, program expects query to be defined as well")]
        public string Evidence { get; set; }

        [Option('m', "markov-blanket", Required = false, HelpText = "If defined, program prints out Markov blanket for entered variable, eg. burglary")]
        public string MarkovBlanketVariableName { get; set; }

        [Option('q', "queries", Required = false, HelpText = "Selected query variables, eg. earthquake,John_calls")]
        public string Queries { get; set; }

        [Option('s', "steps", Required = true, HelpText = "The number of steps performed by MCMC algorithm")]
        public int Steps { get; set; }

        public UserInput ToUserInput() {
            var userInput = new UserInput();

            var networkFile = new FileInfo(NetworkPath);
            if (!networkFile.Exists) {
                ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.InvalidInput, $"File {networkFile.FullName} does not exist");
            }

            using FileStream fs = networkFile.OpenRead();
            using StreamReader sr = new StreamReader(fs);
            var jsonTxt = sr.ReadToEnd();

            var jObj = JObject.Parse(jsonTxt);

            userInput.Network = new BayesianNetwork(jObj);

            if (!string.IsNullOrWhiteSpace(Evidence)) {
                var evidence = JObject.Parse(Evidence);
                userInput.Network.SetEvidence(evidence, out var evidenceNodes);
                userInput.EvidenceNodes = evidenceNodes;
            }

            if (!string.IsNullOrWhiteSpace(MarkovBlanketVariableName)) {
                userInput.MarkovBlanketNode = userInput.Network.Nodes.Single(n => n.Name == MarkovBlanketVariableName);
            }

            if (!string.IsNullOrWhiteSpace(Queries)) {
                var splitedQueries = Queries.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
                userInput.QueryNodes = userInput.Network.Nodes.Where(n => splitedQueries.Contains(n.Name)).ToList();
            }

            userInput.Steps = Steps;

            return userInput;
        }
    }
}
