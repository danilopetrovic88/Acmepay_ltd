namespace Acmepay_ltd.Domain.Models
{
    public class Transaction
    {
        public Transaction()
        {
            Status = TransactionStatus.Authorized.ToString();
        }

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardHolderNumber { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
        public string Status { get; set; }
    }
}

