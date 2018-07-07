namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.OrderDataNormalizationServiceTests
{
    using System;

    using Entities;

    using Factories;

    using FluentAssertions;

    using Helpers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services;

    [TestClass]
    public class OrderStreetNormalizationTests
    {
        private readonly IOrderDataNormalizationService _orderDataNormalizationService;

        public OrderStreetNormalizationTests()
        {
            var orderDataNormalizationServiceFactory = new OrderDataNormalizationServiceFactory();

            this._orderDataNormalizationService = orderDataNormalizationServiceFactory.Create();
        }

        [TestMethod]
        public void Normalize_NullOrderStreet_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithStreet(null);
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null street");
        }

        [TestMethod]
        public void Normalize_OrderWithAbbreviatedStreet_NormalizedStreetExpected()
        {
            const string actualAbbreviatedStreet = "st.";
            const string expectedNormalizedStreet = "street";

            var order = OrderHelper.CreateDefaultWithStreet(actualAbbreviatedStreet);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.Street.Should().Be(expectedNormalizedStreet, "The result street should be normalized to 'street'");
        }

        [TestMethod]
        public void Normalize_OrderWithAbbreviatedRoadInStreet_NormalizedStreetExpected()
        {
            const string actualAbbreviatedStreet = "rd.";
            const string expectedNormalizedStreet = "road";

            var order = OrderHelper.CreateDefaultWithStreet(actualAbbreviatedStreet);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.Street.Should().Be(expectedNormalizedStreet, "The result street should be normalized to 'road'");
        }

        private Order ExecuteTest(Order order)
        {
            return this._orderDataNormalizationService.Normalize(order);
        }
    }
}
