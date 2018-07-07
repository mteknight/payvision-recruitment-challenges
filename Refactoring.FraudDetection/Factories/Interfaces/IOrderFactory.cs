using Payvision.CodeChallenge.Refactoring.FraudDetection.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    public interface IOrderFactory
    {
        Order CreateFromFieldArray(string[] fields);
    }
}