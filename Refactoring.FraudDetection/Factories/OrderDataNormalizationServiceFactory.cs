using Payvision.CodeChallenge.Refactoring.FraudDetection.Services;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    public sealed class OrderDataNormalizationServiceFactory : IOrderDataNormalizationServiceFactory
    {
        public IOrderDataNormalizationService Create()
        {
            return new OrderDataNormalizationService();
        }
    }
}
