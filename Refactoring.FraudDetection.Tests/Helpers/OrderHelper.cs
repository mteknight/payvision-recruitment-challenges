namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.Helpers
{
    using Entities;

    internal static class OrderHelper
    {
        private static readonly Factories.OrderFactory OrderFactoryInstance = new Factories.OrderFactory();

        public static Order CreateDefault()
        {
            const string line = "1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010";
            var fields = line.Split(new[] { ',' });

            return OrderFactoryInstance.CreateFromFieldArray(fields);
        }

        public static Order CreateDefaultWithStreet(string street)
        {
            var order = OrderHelper.CreateDefault();
            order.Street = street;

            return order;
        }

        public static Order CreateDefaultWithState(string state)
        {
            var order = OrderHelper.CreateDefault();
            order.State = state;

            return order;
        }

        public static Order CreateDefaultWithEmail(string email)
        {
            var order = OrderHelper.CreateDefault();
            order.Email = email;

            return order;
        }
    }
}
