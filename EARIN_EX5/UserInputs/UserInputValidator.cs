namespace EARIN_EX5.UserInputs {
    class UserInputValidator {
        private readonly UserInput userInput;

        public UserInputValidator(UserInput userInput) {
            this.userInput = userInput;
        }

        /* public void Validate()
         {
             ValidateIsBVector();
             ValidateAMatrixSize();
             ValidateBVectorSize();
             ValidateDimensions();
             ValidateDnum();
             ValidatePopulationSize();
             ValidateCrossoverProb();
             ValidateMutationProb();
             ValidateIterations();
         }

         private void ValidateIsBVector()
         {
             if (userInput.B.ndim != 1)
             {
                 Error($"Invalid vector B dimensions");
             }
         }

         private void ValidateAMatrixSize()
         {
             var A = userInput.A;
             var dimensions = userInput.Dimensions;

             if (A.shape.Dimensions.Length != 2 || A.shape[0] != dimensions || A.shape[1] != dimensions)
             {
                 Error($"Invalid matrix A dimensions. For dimensions={dimensions}, A should be a {dimensions}x{dimensions} matrix");
             }
         }

         private void ValidateBVectorSize()
         {
             var B = userInput.B;
             var dimensions = userInput.Dimensions;

             if (B.size != dimensions)
             {
                 Error($"Invalid vector B size. B should be 1x{dimensions} vector");
             }
         }

         private void ValidateDnum()
         {
             var d = userInput.D;

             if (d < 1)
             {
                 Error($"Invalid d, should be >= 1");
             }
         }

         private void ValidateDimensions()
         {
             var dimensions = userInput.Dimensions;

             if (dimensions < 1)
             {
                 Error($"Invalid Dimensions, should be >= 1");
             }
         }

         private void ValidatePopulationSize()
         {
             var populationSize = userInput.PopulationSize;

             if (populationSize < 2 || populationSize % 2 == 1)
             {
                 Error($"Invalid population size, has to be >= 2 and even");
             }
         }

         private void ValidateCrossoverProb()
         {
             var crossoverProb = userInput.CrossoverProb;

             if (crossoverProb < 0 || crossoverProb > 1)
             {
                 Error($"Invalid crossover probability, has to be > 0 and < 1");
             }
         }

         private void ValidateMutationProb()
         {
             var mutationProb = userInput.CrossoverProb;

             if (mutationProb < 0 || mutationProb > 1)
             {
                 Error($"Invalid mutation probability, has to be > 0 and < 1");
             }
         }

         private void ValidateIterations()
         {
             var iterations = userInput.Iterations;

             if (iterations < 1)
             {
                 Error($"Number of iterations has to be >= 1");
             }
         }

         private void Error(string message)
         {
             ExceptionHelper.ThrowAndExit(ExceptionHelper.ExceptionType.ValidationError, message);
         }*/
    }
}
