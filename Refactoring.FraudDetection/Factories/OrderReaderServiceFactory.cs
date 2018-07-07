using Payvision.CodeChallenge.Refactoring.FraudDetection.Services;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    public sealed class OrderReaderServiceFactory
    {
        public OrderReaderService Create()
        {
            var orderFactory = new OrderFactory();
            return new OrderReaderService(orderFactory);
        }
    }
}
