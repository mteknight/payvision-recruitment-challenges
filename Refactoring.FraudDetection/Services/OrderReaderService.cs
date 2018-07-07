namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Entities;

    public class OrderReaderService : IOrderReaderService
    {
        private const string PathNullOrEmptyErrorMessage = "Path cannot be null or empty when reading orders.";

        public IEnumerable<Order> ReadOrders(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), PathNullOrEmptyErrorMessage);
            }

            var orders = new List<Order>();

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var order = new Order
                {
                    OrderId = int.Parse(items[0]),
                    DealId = int.Parse(items[1]),
                    Email = items[2].ToLower(),
                    Street = items[3].ToLower(),
                    City = items[4].ToLower(),
                    State = items[5].ToLower(),
                    ZipCode = items[6],
                    CreditCard = items[7]
                };

                yield return Normalize(order);
            }
        }

        private static Order Normalize(Order order)
        {
            //Normalize email
            var aux = order.Email.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0]
                .IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0
                ? aux[0]
                    .Replace(".", "")
                : aux[0]
                    .Replace(".", "")
                    .Remove(atIndex);

            order.Email = string.Join("@", aux[0], aux[1]);

            //Normalize street
            order.Street = order.Street.Replace("st.", "street")
                .Replace("rd.", "road");

            //Normalize state
            order.State = order.State.Replace("il", "illinois")
                .Replace("ca", "california")
                .Replace("ny", "new york");

            return order;
        }
    }
}
