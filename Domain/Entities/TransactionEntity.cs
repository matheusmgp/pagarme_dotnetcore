

using Domain.Validations;

namespace Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {

        public double Price { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string OwnerName { get; set; }
        public PayableEntity Payable { get; set; }
        public DateTime CardExpiresDate { get; set; }
        public int Cvv { get; set; }
        private TransactionEntity(double price, DateTime cardExpiresDate, string description, string paymentMethod, string cardNumber,
             string ownerName, int cvv)
        {
            Price = price;
            CardExpiresDate = cardExpiresDate;
            Validate(description, paymentMethod, cardNumber, ownerName, cvv);
        }

        private void Validate(string description, string paymentMethod, string cardNumber,
             string ownerName, int cvv)
        {
            DomainValidationException.When(string.IsNullOrEmpty(description), "Description deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(paymentMethod), "PaymentMethod deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(cardNumber), "CardNumber deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(ownerName), "OwnerName deve ser informado");
            DomainValidationException.When(cvv <= 0, "CVV deve ser informado");
            Description = description;
            PaymentMethod = paymentMethod;
            CardNumber = cardNumber;
            OwnerName = ownerName;
            Cvv = cvv;
            CardNumber = Masked(CardNumber);


        }
        public static string Masked(string input)
        {

            int len = input.Length;
            string firstPart = input.Substring(0, len - 4);

            string lastPart = input.Substring(len - 4, 4);

            string middlePart = new String('*', firstPart.Length);

            return middlePart + lastPart;
        }

        public static TransactionEntity CreateEntity(TransactionEntityProps props)
        {
            return new TransactionEntity(props.Price, props.CardExpiresDate, props.Description, props.PaymentMethod, props.CardNumber, props.OwnerName, props.Cvv);

        }

    }
    public class TransactionEntityProps
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string CardNumber { get; set; }
        public string OwnerName { get; set; }
        public DateTime CardExpiresDate { get; set; }
        public int Cvv { get; set; }

        public TransactionEntityProps(double price, DateTime cardExpiresDate, string description, string paymentMethod, string cardNumber, string ownerName, int cvv)
        {
            Price = price;
            Description = description;
            PaymentMethod = paymentMethod;
            OwnerName = ownerName;
            CardNumber = cardNumber;
            CardExpiresDate = cardExpiresDate;
            Cvv = cvv;
        }
    }
}
