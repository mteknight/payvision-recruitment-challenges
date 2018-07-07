using System.Linq;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    using System;
    using System.Collections.Generic;

    using Entities;

    using Services;

    using ValueObjects;

    public class FraudRadar
    {
        private readonly IOrderReaderService _orderReaderService;

        public FraudRadar(
            IOrderReaderService orderReaderService)
        {
            _orderReaderService = orderReaderService ?? throw new ArgumentNullException(nameof(orderReaderService));
        }

        public IEnumerable<FraudResult> Check(string filePath)
        {
            var orders = _orderReaderService.ReadOrders(filePath);

            return GetFrauds(orders.ToList());
        }

        private static IEnumerable<FraudResult> GetFrauds(List<Order> orders)
        {
            for (var i = 0; i < orders.Count; i++)
            {
                var current = orders[i];
                var isFraudulent = false;

                for (var j = i + 1; j < orders.Count; j++)
                {
                    isFraudulent = false;

                    if (current.DealId == orders[j].DealId
                        && current.Email == orders[j].Email
                        && current.CreditCard != orders[j].CreditCard)
                        isFraudulent = true;

                    if (current.DealId == orders[j].DealId
                        && current.State == orders[j].State
                        && current.ZipCode == orders[j].ZipCode
                        && current.Street == orders[j].Street
                        && current.City == orders[j].City
                        && current.CreditCard != orders[j].CreditCard)
                        isFraudulent = true;

                    if (isFraudulent)
                        yield return new FraudResult
                        {
                            IsFraudulent = true,
                            OrderId = orders[j]
                                .OrderId
                        };
                }
            }
        }
    }
}
