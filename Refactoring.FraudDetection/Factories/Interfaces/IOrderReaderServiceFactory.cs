using Payvision.CodeChallenge.Refactoring.FraudDetection.Services;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    public interface IOrderReaderServiceFactory
    {
        IOrderReaderService Create();
    }
}