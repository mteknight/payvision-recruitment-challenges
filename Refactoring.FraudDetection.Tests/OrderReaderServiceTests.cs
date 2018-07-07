namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services;

    [TestClass]
    public class OrderReaderServiceTests
    {
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

        private static List<Order> ExecuteTest(string filePath)
        {
            var orderReader = new OrderReaderService();

            return orderReader.ReadOrders(filePath).ToList();
        }
    }
}
