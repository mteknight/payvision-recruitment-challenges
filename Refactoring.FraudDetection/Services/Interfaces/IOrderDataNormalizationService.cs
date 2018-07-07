using Payvision.CodeChallenge.Refactoring.FraudDetection.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Services
{
    public interface IOrderDataNormalizationService
    {
        Order Normalize(Order order);
    }
}