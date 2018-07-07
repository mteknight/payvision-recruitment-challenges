namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int DealId { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string CreditCard { get; set; }
    }
}
