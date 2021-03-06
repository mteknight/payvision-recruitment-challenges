﻿using System.IO;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Payvision.CodeChallenge.Refactoring.FraudDetection.Factories;

    [TestClass]
    public class OrderReaderServiceTests
    {
        private readonly IOrderReaderServiceFactory _orderReaderServiceFactory;

        public OrderReaderServiceTests()
        {
            this._orderReaderServiceFactory = new OrderReaderServiceFactory();
        }

        [TestMethod]
        public void ReadOrders_NullFileStream_ExceptionExpected()
        {
            var result = new Action(() => ExecuteTest(null));

            result.ShouldThrowExactly<ArgumentNullException>("The result should not read from empty file path");
        }

        private List<Order> ExecuteTest(FileStream ordersFileStream)
        {
            var orderReader = this._orderReaderServiceFactory.Create();

            return orderReader.ReadOrders(ordersFileStream).ToList();
        }
    }
}
