using Application.Dtos;
using Application.Errors;
using Application.Services.Contracts;
using Application.Utils;
using Application.Validations;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPayableService _payableService;
        private readonly IMapper _mapper;


        public TransactionService(ITransactionRepository transactionRepository, IPayableService payableService, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _payableService = payableService;
            _mapper = mapper;
        }



        public async Task<ResultService<TransactionDto>> CreateAsync(TransactionDto transactionDto)
        {
            try
            {
                if (transactionDto == null)
                {
                    return ResultService.Fail<TransactionDto>("Objeto deve ser informado");
                }

                var result = new TransactionValidator().Validate(transactionDto);

                if (!result.IsValid) return ResultService.RequestError<TransactionDto>("Problemas de validação ", result);

                var transactionEntity = _mapper.Map<TransactionEntity>(transactionDto);

                var price = transactionEntity.Price;
                var paymentMethod = transactionEntity.PaymentMethod;
                var transaction = await _transactionRepository.Create(transactionEntity);

                await _payableService.CreateAsync(new PayableDto(CalculateFee(paymentMethod, price), SetPaymentDate(paymentMethod), SetStatus(paymentMethod), SetAvailability(paymentMethod), transaction.Id));

                return ResultService.Ok(_mapper.Map<TransactionDto>(transaction));
            }
            catch (Exception ex)
            {
                var errorType = ex.GetType().Name;

                if (errorType == "DatabaseException")
                {
                    throw new DatabaseException(ex.Message);
                }
                else if (errorType == "InternalServerException")
                {
                    throw new InternalServerException(ex.Message);
                }
                else
                {
                    throw new BadRequestException(ex.Message);
                }
            }
        }

        public async Task<ResultService<ICollection<TransactionDto>>> GetAll()
        {
            try
            {
                var transactions = await _transactionRepository.GetAll();
                return ResultService.Ok(_mapper.Map<ICollection<TransactionDto>>(transactions));
            }

            catch (Exception ex)
            {
                var errorType = ex.GetType().Name;
                if (errorType == "DatabaseException")
                {
                    throw new DatabaseException(ex.Message);

                }else if (errorType == "InternalServerException")
                {
                    throw new InternalServerException(ex.Message);
                }
                else
                {
                    throw new BadRequestException(ex.Message);
                }
                
            }
            
        }

        public string SetStatus(string paymentMethod)
        {
            return paymentMethod == PaymentMethodEnum.DEBIT ? "paid" : PayableStatusEnum.WAITING_FUNDS;
        }
        public DateTime SetPaymentDate(string paymentMethod)
        {
            DateTime date = DateTime.Now;
            return paymentMethod == PaymentMethodEnum.DEBIT ? DateTime.Now : date.AddDays(30);
        }
        public double CalculateFee(string paymentMethod, double transactionPrice)
        {
            if (paymentMethod == PaymentMethodEnum.DEBIT)
            {
                return transactionPrice - transactionPrice * 0.03;
            }
            else
            {
                return transactionPrice - transactionPrice * 0.05;
            }
        }
        public string SetAvailability(string paymentMethod)
        {
            return paymentMethod == PaymentMethodEnum.DEBIT ? PayableStatusEnum.AVAILABLE : PayableStatusEnum.WAITING_FUNDS;
        }


    }
}
