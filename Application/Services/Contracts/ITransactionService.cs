using Application.Dtos;

namespace Application.Services.Contracts
{
    public interface ITransactionService : IBaseService<TransactionDto>
    {
        string SetStatus(string paymentMethod);
        DateTime SetPaymentDate(string paymentMethod);
        double CalculateFee(string paymentMethod, double transactionPrice);
        string SetAvailability(string paymentMethod);
    }
}
