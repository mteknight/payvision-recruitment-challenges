using System.Linq;

using Payvision.CodeChallenge.Refactoring.FraudDetection.Factories;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Entities;

    public class OrderReaderService : IOrderReaderService
    {
        private readonly OrderFactory _orderFactory;

        public OrderReaderService(
            OrderFactory orderFactory)
        {
            _orderFactory = orderFactory ?? throw new ArgumentNullException(nameof(orderFactory));
        }

        public IEnumerable<Order> ReadOrders(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                const string PathNullOrEmptyErrorMessage = "Path cannot be null or empty when reading orders.";
                throw new ArgumentNullException(nameof(filePath), PathNullOrEmptyErrorMessage);
            }

            var lines = ReadOrdersFile(filePath);
            foreach (var line in lines)
            {
                yield return ParseOrder(line);
            }
        }

        private static IEnumerable<string> ReadOrdersFile(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath)
                    .Where(line => !string.IsNullOrEmpty(line));
            }
            catch (Exception e)
            {
                LogException(e);
                return Array.Empty<string>();
            }
        }

        private static void LogException(Exception e)
        {
            Console.WriteLine(e);
        }

        private static string[] ParseOrderFields(string line)
        {
            return line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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

        private Order ParseOrder(string line)
        {
            var fields = ParseOrderFields(line);
            var order = _orderFactory.CreateFromFieldArray(fields);

            return Normalize(order);
        }
    }
}
