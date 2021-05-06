using CommandLine;
using EARIN_EX5.Algorithm;
using EARIN_EX5.Entities;
using EARIN_EX5.UserInputs;
using FunctionMinimization.Helpers;
using System;
using System.Linq;

namespace EARIN_EX5 {
    class Program {
        private const string Logo = "+-+-+-+-+-+-+-+-+-+-+\r\n|E|A|R|I|N| |E|X| |5|\r\n+-+-+-+-+-+-+-+-+-+-+\r\nOskar H\u0105cel\r\nMarcin Lisowski\r\nPW, 2021\r\n+-+-+-+-+-+-+-+-+-+-+";

        static void Main(string[] args) {
            PrintLogo();
            GetUserInput(args);
        }

        private static void GetUserInput(string[] args) {
            Parser.Default.ParseArguments<RawUserInput>(args)
                .WithParsed(o => new Program().Start(o.ToUserInput()));
        }

        private void Start(UserInput userInput) {
            new UserInputValidator(userInput).Validate();

            if (userInput.MarkovBlanketNode != null) {
                PrintMarkovBlanket(userInput.MarkovBlanketNode);
            }

            if (userInput.QueryNodes?.Any() == true) {
                var mcmc = new McmcGibbs(userInput);
                mcmc.EvaluateQueries();
            }
        }

        private static void PrintLogo() {
            Console.ForegroundColor = EnumHelper.PickRandom(ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.DarkGray);
            Console.WriteLine(Logo);
            Console.ResetColor();
            Console.WriteLine();
        }

        private void PrintMarkovBlanket(Node node) {
            Console.WriteLine($"Markov blanket for {node.Name}:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Join(", ", node.MarkovBlanket.Select(node => node.Name)));
            Console.ResetColor();
            Console.WriteLine();
        }

        private void PrintUserInput(UserInput userInput) {
            Console.WriteLine();
            Console.WriteLine("Given data:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(userInput.ToString());
            Console.ResetColor();
        }
    }
}
