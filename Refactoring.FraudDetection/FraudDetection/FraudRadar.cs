namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        private static IEnumerable<FraudResult> GetFrauds(IReadOnlyList<Order> orders)
        {
            for (var baseOrderIndex = 0; baseOrderIndex < orders.Count; baseOrderIndex++)
            {
                var baseOrderToCompareTo = orders[baseOrderIndex];

                for (var currentOrderIndex = baseOrderIndex + 1; currentOrderIndex < orders.Count; currentOrderIndex++)
                {
                    var currentOrder = orders[currentOrderIndex];

                    var isFraudulent = IsFraudulentOrder(baseOrderToCompareTo, currentOrder);
                    if (isFraudulent)
                    {
                        yield return CreateFraudResult(currentOrder);
                    }
                }
            }
        }

        private static bool IsFraudulentOrder(Order baseOrderToCompare, Order currentOrder)
        {
            return IsMatchOrderByIdentity(baseOrderToCompare, currentOrder) ||
                   IsMatchOrderByAddress(baseOrderToCompare, currentOrder);
        }

        private static bool IsMatchOrderByIdentity(Order currentOrder, Order order)
        {
            return MatchByDealId(currentOrder, order) &&
                   MatchByEmail(currentOrder, order) &&
                   NotMatchByCreditCard(currentOrder, order);
        }

        private static bool MatchByDealId(Order currentOrder, Order order)
        {
            return currentOrder.DealId == order.DealId;
        }

        private static bool MatchByEmail(Order currentOrder, Order order)
        {
            return currentOrder.Email == order.Email;
        }

        private static bool NotMatchByCreditCard(Order currentOrder, Order order)
        {
            return currentOrder.CreditCard != order.CreditCard;
        }

        private static bool IsMatchOrderByAddress(Order currentOrder, Order order)
        {
            return MatchByDealId(currentOrder, order) &&
                   MatchByAddress(currentOrder, order) &&
                   NotMatchByCreditCard(currentOrder, order);
        }

        private static bool MatchByAddress(Order currentOrder, Order order)
        {
            return currentOrder.State == order.State &&
                   currentOrder.ZipCode == order.ZipCode &&
                   currentOrder.Street == order.Street &&
                   currentOrder.City == order.City;
        }

        private static FraudResult CreateFraudResult(Order fraudulentOrder)
        {
            return new FraudResult
            {
                IsFraudulent = true,
                OrderId = fraudulentOrder.OrderId
            };
        }
    }
}
