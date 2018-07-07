using Payvision.CodeChallenge.Refactoring.FraudDetection.Services;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    public sealed class OrderReaderServiceFactory : IOrderReaderServiceFactory
    {
        public IOrderReaderService Create()
        {
            var orderFactory = new OrderFactory();
            var orderDataNormalizationServiceFactory = new OrderDataNormalizationServiceFactory();
            var orderDataNormalizationService = orderDataNormalizationServiceFactory.Create();

            return new OrderReaderService(
                orderFactory,
                orderDataNormalizationService);
        }
    }
}
