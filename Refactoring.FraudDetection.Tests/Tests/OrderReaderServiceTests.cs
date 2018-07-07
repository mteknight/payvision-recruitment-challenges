namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

    using Factories;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderReaderServiceTests
    {
        private readonly IOrderReaderServiceFactory _orderReaderServiceFactory;

        public OrderReaderServiceTests()
        {
            this._orderReaderServiceFactory = new OrderReaderServiceFactory();
        }

        [TestMethod]
        public void ReadOrders_EmptyFilePath_ExceptionExpected()
        {
            var result = new Action(() => ExecuteTest(string.Empty));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not read from empty file path");
        }

        [TestMethod]
        public void ReadOrders_NullFilePath_ExceptionExpected()
        {
            var result = new Action(() => ExecuteTest(null));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not read from empty file path");
        }

        private List<Order> ExecuteTest(string filePath)
        {
            var orderReader = this._orderReaderServiceFactory.Create();

            return orderReader.ReadOrders(filePath).ToList();
        }
    }
}
