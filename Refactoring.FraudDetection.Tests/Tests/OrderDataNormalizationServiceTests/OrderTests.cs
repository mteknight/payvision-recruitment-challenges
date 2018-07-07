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
    public class OrderTests
    {
        private readonly IOrderDataNormalizationService _orderDataNormalizationService;

        public OrderTests()
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
        public void Normalize_NullOrderEmail_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithEmail(null);
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null email address ");
        }

        [TestMethod]
        public void Normalize_NullOrderStreet_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithStreet(null);
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null street");
        }

        [TestMethod]
        public void Normalize_NullOrderState_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithState(null);
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null state");
        }

        [TestMethod]
        public void Normalize_OrderWithIncompleteEmail_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithEmail("username.domain");
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentException>("The result email address should not be invalid or incomplete");
        }

        [TestMethod]
        public void Normalize_OrderWithEmailWithDot_NormalizedEmailExpected()
        {
            const string actualRawEmail = "username.test@domain";
            const string expectedNormalizedEmail = "usernametest@domain";

            var order = OrderHelper.CreateDefaultWithEmail(actualRawEmail);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.Email.Should().Be(expectedNormalizedEmail, "The result email address should be normalized");
        }

        [TestMethod]
        public void Normalize_OrderWithEmailWithDotAndPlusSign_NormalizedEmailExpected()
        {
            const string actualRawEmail = "user+name.test@domain";
            const string expectedNormalizedEmail = "user@domain";

            var order = OrderHelper.CreateDefaultWithEmail(actualRawEmail);
            var result = ExecuteTest(order);

            result.Should().NotBeNull("The result should not be null.");
            result.Email.Should().Be(expectedNormalizedEmail, "The result email address should be normalized");
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
