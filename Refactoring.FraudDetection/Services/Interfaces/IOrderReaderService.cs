using System.IO;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Services
{
    using System.Collections.Generic;

    using Entities;

    public interface IOrderReaderService
    {
        IEnumerable<Order> ReadOrders(FileStream ordersFileStream);
    }
}
