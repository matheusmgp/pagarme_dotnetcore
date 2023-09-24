

namespace Application.Dtos
{
    public class PayableDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string Availability { get; set; }       
        public int TransactionId { get; set; }

        public PayableDto(double amount,DateTime paymentDate,string status,string availability, int transactionId) {
            Amount = amount;
            PaymentDate = paymentDate;   
            Status = status; 
            Availability = availability;
            TransactionId = transactionId;
        } 
    }
}
