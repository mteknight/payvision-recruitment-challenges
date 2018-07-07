namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    public sealed class FraudRadarFactory : IFraudRadarFactory
    {
        public FraudRadar Create()
        {
            var orderReaderServiceFactory = new OrderReaderServiceFactory();
            var orderReaderService = orderReaderServiceFactory.Create();
            return new FraudRadar(orderReaderService);
        }
    }
}
