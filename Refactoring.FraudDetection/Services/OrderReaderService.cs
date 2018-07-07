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
        private readonly IOrderFactory _orderFactory;
        private readonly IOrderDataNormalizationService _orderDataNormalizationService;

        public OrderReaderService(
            IOrderFactory orderFactory,
            IOrderDataNormalizationService orderDataNormalizationService)
        {
            _orderFactory = orderFactory ?? throw new ArgumentNullException(nameof(orderFactory));
            _orderDataNormalizationService = orderDataNormalizationService ?? throw new ArgumentNullException(nameof(orderDataNormalizationService));
        }

        public IEnumerable<Order> ReadOrders(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                const string pathNullOrEmptyError = "Path cannot be null or empty when reading orders.";
                throw new ArgumentNullException(nameof(filePath), pathNullOrEmptyError);
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

        private Order ParseOrder(string line)
        {
            var fields = ParseOrderFields(line);
            var order = _orderFactory.CreateFromFieldArray(fields);

            return this._orderDataNormalizationService.Normalize(order);
        }

        private static string[] ParseOrderFields(string line)
        {
            return line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
