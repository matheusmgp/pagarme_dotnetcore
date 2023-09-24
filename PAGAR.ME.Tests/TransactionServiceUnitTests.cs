using Application.Services;
using Application.Services.Contracts;
using AutoMapper;
using Domain.Contracts.Repositories;
using Moq;

namespace PAGAR.ME.Tests
{
    public class TransactionServiceTests
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPayableService _payableService;
        private readonly IMapper _mapper;
        public readonly   TransactionService _service;

        public TransactionServiceTests()
        {
            _service = new TransactionService(_transactionRepository, _payableService, _mapper);
        }
        [Fact]
        public void EXPECTED_STATUS_TOBE_EQUALS_PAID()
        {                 
            var status = _service.SetStatus("debit_card");
            Assert.Equal( "paid", status);
        }

        [Fact]
        public void EXPECTED_STATUS_TOBE_EQUALS_WAITING_FUNDS()
        {
            var status = _service.SetStatus("credit_card");
            Assert.Equal("waiting_funds", status);
        }

        [Fact]
        public void EXPECTED_PRICE_TOBE_EQUALS_97()
        {
            var price = _service.CalculateFee("debit_card",100);
            Assert.Equal(97, price);
        }
        [Fact]
        public void EXPECTED_PRICE_TOBE_EQUALS_95()
        {
            var price = _service.CalculateFee("credit_card", 100);
            Assert.Equal(95, price);
        }

        [Fact]
        public void EXPECTED_AVAILABILITY_TOBE_EQUALS_AVAILABLE()
        {
            var availability = _service.SetAvailability("debit_card");
            Assert.Equal("available", availability);
        }
        [Fact]
        public void EXPECTED_AVAILABILITY_TOBE_EQUALS_WAITING_FUNDS()
        {
            var availability = _service.SetAvailability("credit_card");
            Assert.Equal("waiting_funds", availability);
        }
    }
}