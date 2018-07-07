using System;

using Payvision.CodeChallenge.Refactoring.FraudDetection.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Services
{
    public class OrderDataNormalizationService : IOrderDataNormalizationService
    {
        public Order Normalize(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null for data normalization");
            }

            order.Email = NormalizeEmail(order);
            order.Street = NormalizeStreet(order);
            order.State = NormalizeState(order);

            return order;
        }

        private static string NormalizeEmail(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.Email))
            {
                return order.Email;
            }

            var emailParts = SplitEmailParts(order.Email);

            emailParts[0] = StripDotsAndPlusSign(emailParts[0]);

            return ReconstructNormalizedEmail(emailParts);
        }

        private static string[] SplitEmailParts(string email)
        {
            const string invalidEmailAddressError = "Email addresses must be composed by a username and domain address.";

            var emailParts = email.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            return emailParts.Length == 2
                ? emailParts
                : throw new ArgumentException(invalidEmailAddressError, nameof(emailParts));
        }

        private static string StripDotsAndPlusSign(string leftEmailPart)
        {
            var indexOfPlusSign = GetPlusSignPosition(leftEmailPart);

            return indexOfPlusSign < 0
                ? RemoveDots(leftEmailPart)
                : RemoveDotsAndPlusSign(leftEmailPart, indexOfPlusSign);
        }

        private static int GetPlusSignPosition(string leftEmailPart)
        {
            return leftEmailPart.IndexOf("+", StringComparison.Ordinal);
        }

        private static string RemoveDots(string leftEmailPart)
        {
            return leftEmailPart
                .Replace(".", "");
        }

        private static string RemoveDotsAndPlusSign(string leftEmailPart, int indexOfPlusSign)
        {
            return RemoveDots(leftEmailPart)
                .Remove(indexOfPlusSign);
        }

        private static string ReconstructNormalizedEmail(string[] emailParts)
        {
            return string.Join("@", emailParts[0], emailParts[1]);
        }

        private static string NormalizeStreet(Order order)
        {
            return order.Street
                .Replace("st.", "street")
                .Replace("rd.", "road");
        }

        private static string NormalizeState(Order order)
        {
            return order.State
                .Replace("il", "illinois")
                .Replace("ca", "california")
                .Replace("ny", "new york");
        }
    }
}
