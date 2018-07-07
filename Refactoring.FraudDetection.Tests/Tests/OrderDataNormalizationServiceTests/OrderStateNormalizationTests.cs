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
    public class OrderStateNormalizationTests
    {
        private readonly IOrderDataNormalizationService _orderDataNormalizationService;

        public OrderStateNormalizationTests()
        {
            var orderDataNormalizationServiceFactory = new OrderDataNormalizationServiceFactory();

            this._orderDataNormalizationService = orderDataNormalizationServiceFactory.Create();
        }

        [TestMethod]
        public void Normalize_NullOrderState_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithState(null);
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null state");
        }

        [TestMethod]
        public void Normalize_OrderWithAbbreviatedIllinoisState_NormalizedIllinoisStateExpected()
        {
            const string actualAbbreviatedState = "il";
            const string expectedNormalizedState = "illinois";

            var order = OrderHelper.CreateDefaultWithState(actualAbbreviatedState);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.State.Should().Be(expectedNormalizedState, "The result state should be normalized to 'illinois'");
        }

        [TestMethod]
        public void Normalize_OrderWithAbbreviatedCaliforniaState_NormalizedCaliforniaStateExpected()
        {
            const string actualAbbreviatedState = "ca";
            const string expectedNormalizedState = "california";

            var order = OrderHelper.CreateDefaultWithState(actualAbbreviatedState);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.State.Should().Be(expectedNormalizedState, "The result state should be normalized to 'california'");
        }

        [TestMethod]
        public void Normalize_OrderWithAbbreviatedNewYorkState_NormalizedNewYorkStateExpected()
        {
            const string actualAbbreviatedState = "ny";
            const string expectedNormalizedState = "new york";

            var order = OrderHelper.CreateDefaultWithState(actualAbbreviatedState);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.State.Should().Be(expectedNormalizedState, "The result state should be normalized to 'new york'");
        }

        private Order ExecuteTest(Order order)
        {
            return this._orderDataNormalizationService.Normalize(order);
        }
    }
}
