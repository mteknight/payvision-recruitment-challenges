using System.Text;

using Payvision.CodeChallenge.Refactoring.FraudDetection.Extensions;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Entities;

    using Factories;

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

        public IEnumerable<Order> ReadOrders(FileStream ordersFileStream)
        {
            if (ordersFileStream == null)
            {
                throw new ArgumentNullException(nameof(ordersFileStream));
            }

            var lines = ReadOrdersFromStream(ordersFileStream);
            foreach (var line in lines)
            {
                yield return ParseOrder(line);
            }
        }

        private static IEnumerable<string> ReadOrdersFromStream(FileStream ordersFileStream)
        {
            try
            {
                var fileContents = ReadFileContents(ordersFileStream);
                return BreakLines(fileContents);
            }
            catch (Exception e)
            {
                LogException(e);
                return Array.Empty<string>();
            }
            finally
            {
                ordersFileStream.Close();
            }
        }

        private static string ReadFileContents(FileStream ordersFileStream)
        {
            var fileBytes = ordersFileStream.ReadAllBytes();
            return Encoding.UTF8.GetCleanString(fileBytes);
        }

        private static IEnumerable<string> BreakLines(string fileContents)
        {
            return fileContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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
