namespace EARIN_EX5.Entities {
    class Probability {
        public Probability(string concatedNames, double value) {
            ConcatedNames = concatedNames;
            Value = value;
        }

        public string ConcatedNames { get; }
        public double Value { get; }
    }
}
