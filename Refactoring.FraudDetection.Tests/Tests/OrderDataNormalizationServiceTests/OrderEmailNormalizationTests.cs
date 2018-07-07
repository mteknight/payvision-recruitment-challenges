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
    public class OrderEmailNormalizationTests
    {
        private readonly IOrderDataNormalizationService _orderDataNormalizationService;

        public OrderEmailNormalizationTests()
        {
            var orderDataNormalizationServiceFactory = new OrderDataNormalizationServiceFactory();

            this._orderDataNormalizationService = orderDataNormalizationServiceFactory.Create();
        }

        [TestMethod]
        public void Normalize_NullOrderEmail_ExceptionExpected()
        {
            var order = OrderHelper.CreateDefaultWithEmail(null);
            var result = new Action(() => ExecuteTest(order));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain a null email address ");
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

        private Order ExecuteTest(Order order)
        {
            return this._orderDataNormalizationService.Normalize(order);
        }
    }
}
