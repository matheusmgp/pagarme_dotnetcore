

using Domain.Validations;

namespace Domain.Entities
{
    public class PayableEntity : BaseEntity
    {

        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string Availability { get; set; }
        public TransactionEntity Transaction { get; set; }
        public int TransactionId { get; set; }

        private PayableEntity(double amount, DateTime paymentDate, string status, string availability, int transactionId)
        {
            Amount = amount;
            PaymentDate = paymentDate;
            Validate(status, availability, transactionId);
        }

        private void Validate(string status, string availability, int transactionId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(status), "Status deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(availability), "Availability deve ser informado");
            DomainValidationException.When(transactionId <= 0, "TransactionId deve ser informado");
            Status = status;
            Availability = availability;
            TransactionId = transactionId;
        }
        public static PayableEntity CreateEntity(PayableEntityProps props)
        {
            return new PayableEntity(props.Amount, props.PaymentDate, props.Status, props.Availability, props.TransactionId);

        }
    }

    public class PayableEntityProps
    {
        public double Amount;
        public DateTime PaymentDate;
        public string Status;
        public string Availability;
        public int TransactionId;

        public PayableEntityProps(double amount, DateTime paymentDate, string status, string availability, int transactionId)
        {
            Amount = amount;
            PaymentDate = paymentDate;
            Status = status;
            Availability = availability;
            TransactionId = transactionId;
        }
    }

}
