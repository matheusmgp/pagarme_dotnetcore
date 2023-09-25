

namespace Application.Dtos
{
    public class TransactionDto
    {
        public TransactionDto(double price,string description,string paymentMethod,string cardNumber, string ownerName, DateTime cardExpiresDate, int cvv){
            Price  = price;
            Description  = description;
            PaymentMethod  = paymentMethod;
            CardNumber  = cardNumber;
            OwnerName  = ownerName;
            CardExpiresDate  = cardExpiresDate;
            Cvv  = cvv;

        }
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string OwnerName { get; set; }
        public DateTime CardExpiresDate { get; set; }
        public int Cvv { get; set; }
    }
}
