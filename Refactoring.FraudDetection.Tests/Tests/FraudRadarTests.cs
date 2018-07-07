// -----------------------------------------------------------------------
// <copyright file="FraudRadarTests.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Payvision.CodeChallenge.Refactoring.FraudDetection.Factories;

    using ValueObjects;

    [TestClass]
    public class FraudRadarTests
    {
        private readonly IFraudRadarFactory _fraudRadarFactory;

        public FraudRadarTests()
        {
            this._fraudRadarFactory = new FraudRadarFactory();
        }

        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt");
            var ordersFileStream = File.OpenRead(filePath);
            var result = ExecuteTest(ordersFileStream);

            result.Should().NotBeNull("The result should not be null.");
            result.Count().ShouldBeEquivalentTo(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Files", "TwoLines_FraudulentSecond.txt");
            var ordersFileStream = File.OpenRead(filePath);
            var result = ExecuteTest(ordersFileStream);

            result.Should().NotBeNull("The result should not be null.");
            result.Count().ShouldBeEquivalentTo(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/ThreeLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_ThreeLines_SecondLineFraudulent()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Files", "ThreeLines_FraudulentSecond.txt");
            var ordersFileStream = File.OpenRead(filePath);
            var result = ExecuteTest(ordersFileStream);

            result.Should().NotBeNull("The result should not be null.");
            result.Count().ShouldBeEquivalentTo(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/FourLines_MoreThanOneFraudulent.txt", "Files")]
        public void CheckFraud_FourLines_MoreThanOneFraudulent()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Files", "FourLines_MoreThanOneFraudulent.txt");
            var ordersFileStream = File.OpenRead(filePath);
            var result = ExecuteTest(ordersFileStream);

            result.Should().NotBeNull("The result should not be null.");
            result.Count().ShouldBeEquivalentTo(2, "The result should contains the number of lines of the file");
        }

        private List<FraudResult> ExecuteTest(FileStream ordersFileStream)
        {
            var fraudRadar = this._fraudRadarFactory.Create();

            return fraudRadar.Check(ordersFileStream).ToList();
        }
    }
}
