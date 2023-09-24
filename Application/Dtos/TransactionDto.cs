

namespace Application.Dtos
{
    public class TransactionDto
    {
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
