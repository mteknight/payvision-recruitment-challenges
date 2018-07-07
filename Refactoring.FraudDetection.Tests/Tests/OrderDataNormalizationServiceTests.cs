namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    using System;

    using Entities;

    using Factories;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services;

    [TestClass]
    public class OrderDataNormalizationServiceTests
    {
        private readonly IOrderDataNormalizationService _orderDataNormalizationService;

        public OrderDataNormalizationServiceTests()
        {
            var orderDataNormalizationServiceFactory = new OrderDataNormalizationServiceFactory();
            this._orderDataNormalizationService = orderDataNormalizationServiceFactory.Create();
        }

        [TestMethod]
        public void Normalize_NullOrder_ExceptionExpected()
        {
            var result = new Action(() => ExecuteTest(null));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null order");
        }

        [TestMethod]
        public void Normalize_OrderWithEmptyEmail_EmptyValueExpected()
        {
            var order = new Order { Email = string.Empty };
            var result = ExecuteTest(order);

            result.Email.Should().Be(string.Empty, "The result email address should not be normalized when empty");
        }

        [TestMethod]
        public void Normalize_OrderWithIncompleteEmail_ExceptionExpected()
        {
            var order = new Order { Email = "username.domain" };
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentException>("The result email address should not be invalid or incomplete");
        }

        private Order ExecuteTest(Order order)
        {
            return this._orderDataNormalizationService.Normalize(order);
        }
    }
}
