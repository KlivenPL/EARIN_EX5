using Numpy;
using System;
using System.Linq;

namespace EARIN_EX5.UserInputs
{
    public static class NDarrayParser
    {
        public static bool TryParseMatrix(string str, out NDarray matrix)
        {
            matrix = null;
            try
            {
                matrix = ParseMatrix(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static NDarray ParseMatrix(string str)
        {
            var split = str
                .Replace(" ", "")
                .Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (split?.Any() != true)
            {
                throw new ArgumentException();
            }

            var arrays = split.Select(spl => Parse1DArray(spl)).ToArray();
            return np.stack(arrays);
        }

        public static bool TryParse1DArray(string str, out NDarray vector)
        {
            vector = null;
            try
            {
                vector = Parse1DArray(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static NDarray Parse1DArray(string str)
        {
            var ndArray = (NDarray)str.Split(',').Select(n => double.Parse(n)).ToArray();
            return ndArray;
        }
    }
}
