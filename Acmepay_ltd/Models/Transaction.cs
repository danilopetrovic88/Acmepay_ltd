namespace Acmepay_ltd.Models
{
    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
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



//{
//    "amount": 999.99,
//  "currency": "RSD",
//  "cardHolderNumber": "123456789123456789",
//  "holderName": "Danilo Petrovic",
//  "expirationMonth": 12,
//  "expirationYear": 2025,
//  "cvv": 123456,
//  "orderReference": "asdfasdf",
//  "status" : "authorized";
//}
