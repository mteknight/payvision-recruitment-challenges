namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    using System;

    using Entities;

    using Factories;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderFactoryTests
    {
        private readonly IOrderFactory _orderFactory;

        public OrderFactoryTests()
        {
            this._orderFactory = new OrderFactory();
        }

        [TestMethod]
        public void CreateFromFieldArray_MissingIdField_ExceptionExpected()
        {
            var fields = new[] { "1" };
            var result = new Action(() => ExecuteTest(fields));

            result.ShouldThrowExactly<ArgumentOutOfRangeException>("The result should contain at least the id fields");
        }

        [TestMethod]
        public void CreateFromFieldArray_OrderIdIsEmpty_ExceptionExpected()
        {
            var fields = new[] { string.Empty, "1" };
            var result = new Action(() => ExecuteTest(fields));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain ids that are empty or null");
        }

        [TestMethod]
        public void CreateFromFieldArray_DealIdIsEmpty_ExceptionExpected()
        {
            var fields = new[] { "1", string.Empty };
            var result = new Action(() => ExecuteTest(fields));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not contain ids that are empty or null");
        }

        [TestMethod]
        public void CreateFromFieldArray_OrderIdIsNotValidInteger_ExceptionExpected()
        {
            var orderId = (long)int.MaxValue + 1;
            var fields = new[] { orderId.ToString(), "1" };
            var result = new Action(() => ExecuteTest(fields));

            result.ShouldThrowExactly<ArgumentOutOfRangeException>("The result should not contain a OrderId field that is not a valid integer");
        }

        [TestMethod]
        public void CreateFromFieldArray_DealIdIsNotValidInteger_ExceptionExpected()
        {
            var dealId = (long)int.MaxValue + 1;
            var fields = new[] { $"1", dealId.ToString() };
            var result = new Action(() => ExecuteTest(fields));

            result.ShouldThrowExactly<ArgumentOutOfRangeException>("The result should not contain a DealId field that is not a valid integer");
        }

        private Order ExecuteTest(string[] fields)
        {
            return this._orderFactory.CreateFromFieldArray(fields);
        }
    }
}
