using System;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Factories
{
    using Entities;

    public sealed class OrderFactory
    {
        public Order CreateFromFieldArray(string[] fields)
        {
            AssertArrayContainsAtLeastTheIds(fields);
            AssertOrderIdFieldIsValidInteger(fields);
            AssertDealIdFieldIsValidInteger(fields);

            return CreateOrderFromFields(fields);
        }

        private static void AssertArrayContainsAtLeastTheIds(string[] fields)
        {
            if (fields.Length < 2)
            {
                const string fieldsDoNotContainIdsError = "Order fields must have at least the ids.";
                throw new ArgumentOutOfRangeException(nameof(fields), fieldsDoNotContainIdsError);
            }
        }

        private static void AssertOrderIdFieldIsValidInteger(string[] fields)
        {
            if (string.IsNullOrWhiteSpace(fields[0]))
            {
                const string orderIdNullOrEmptyError = "OrderId field must not be null or empty.";
                throw new ArgumentNullException(nameof(fields), orderIdNullOrEmptyError);
            }

            if (!int.TryParse(fields[0], out _))
            {
                const string OrderIdNotValidIntegerError = "OrderId must be a valid integer number.";
                throw new ArgumentOutOfRangeException(nameof(fields), OrderIdNotValidIntegerError);
            }
        }

        private static void AssertDealIdFieldIsValidInteger(string[] fields)
        {
            if (string.IsNullOrWhiteSpace(fields[1]))
            {
                const string dealIdNullOrEmptyError = "DealId field must not be null or empty.";
                throw new ArgumentNullException(nameof(fields), dealIdNullOrEmptyError);
            }

            if (!int.TryParse(fields[1], out _))
            {
                const string DealIdNotValidIntegerError = "DealId must be a valid integer number.";
                throw new ArgumentOutOfRangeException(nameof(fields), DealIdNotValidIntegerError);
            }
        }

        private static Order CreateOrderFromFields(string[] fields)
        {
            return new Order
            {
                OrderId = int.Parse(fields[0]),
                DealId = int.Parse(fields[1]),
                Email = GetValueOrEmpty(fields, 2).ToLower(),
                Street = GetValueOrEmpty(fields, 3).ToLower(),
                City = GetValueOrEmpty(fields, 4).ToLower(),
                State = GetValueOrEmpty(fields, 5).ToLower(),
                ZipCode = GetValueOrEmpty(fields, 6),
                CreditCard = GetValueOrEmpty(fields, 7)
            };
        }

        private static string GetValueOrEmpty(string[] fields, int index)
        {
            return IsValidFieldIndexAndValue(fields, index)
                ? fields[index]
                : string.Empty;
        }

        private static bool IsValidFieldIndexAndValue(string[] fields, int index)
        {
            return fields.Length >= index &&
                   !string.IsNullOrEmpty(fields[index]);
        }
    }
}
