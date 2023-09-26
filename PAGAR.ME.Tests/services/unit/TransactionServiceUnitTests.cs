using System.Collections.ObjectModel;
using Application.Dtos;
using Application.Mappers;
using Application.Services;
using Application.Services.Contracts;
using Application.Utils;
using AutoFixture;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Moq;
using PAGAR.ME.Tests.Fixtures;

namespace PAGAR.ME.Tests
{
    public class TransactionServiceUnitTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
        private readonly Mock<IPayableService> _payableService = new Mock<IPayableService>();

        private readonly IMapper _map;
        public readonly TransactionService sut;

        public TransactionServiceUnitTests()
        {
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ConfigureAutoMapper());
            });
            _map = config.CreateMapper();
            sut = new TransactionService(_transactionRepository.Object, _payableService.Object, _map);
        }
        [Fact]
        public void TransactionService_SetStatus_Method_Paid()
        {
            var status = sut.SetStatus("debit_card");
            Assert.Equal("paid", status);
        }

        [Fact]
        public void TransactionService_SetStatus_Method_Waiting_Funds()
        {
            var status = sut.SetStatus("credit_card");
            Assert.Equal("waiting_funds", status);
        }

        [Fact]
        public void TransactionService_CalculateFee_Method_Debit()
        {
            var price = sut.CalculateFee("debit_card", 100);
            Assert.Equal(97, price);
        }
        [Fact]
        public void TransactionService_CalculateFee_Method_Credit()
        {
            var price = sut.CalculateFee("credit_card", 100);
            Assert.Equal(95, price);
        }

        [Fact]
        public void TransactionService_SetAvailability_Method_Debit()
        {
            var availability = sut.SetAvailability("debit_card");
            Assert.Equal("available", availability);
        }
        [Fact]
        public void TransactionService_SetAvailability_Method_Credit()
        {
            var availability = sut.SetAvailability("credit_card");
            Assert.Equal("waiting_funds", availability);
        }

        [Fact]
        public async void TransactionService_CreateAsync_Method()
        {

            var transactionDto = TransactionServiceFixtures.TransactionDtoFixture();
            var transactionEntity = TransactionServiceFixtures.TransactionEntityFixture();
            var payableDto = TransactionServiceFixtures.PayableDtoFixture();

            _payableService.Setup(y => y.CreateAsync(It.IsAny<PayableDto>())).ReturnsAsync(ResultService.Ok(payableDto));
            _transactionRepository.Setup(x => x.Create(It.IsAny<TransactionEntity>())).ReturnsAsync(transactionEntity);

            var result = await sut.CreateAsync(transactionDto);
            Assert.Equal(transactionDto.Price, result.Data.Price);
            Assert.Equal(transactionDto.Description, result.Data.Description);
            Assert.Equal(transactionDto.PaymentMethod, result.Data.PaymentMethod);
            Assert.Equal(transactionDto.OwnerName, result.Data.OwnerName);
            Assert.Equal("************1234", result.Data.CardNumber);
            Assert.Equal(transactionDto.Cvv, result.Data.Cvv);
        }
        [Fact]
        public async void TransactionService_GetAll_Method()
        {
            var transactions = TransactionServiceFixtures.TransactionEntityListFixture();

            _transactionRepository.Setup(x => x.GetAll()).ReturnsAsync(transactions);

            var results = await sut.GetAll();
            Assert.Contains(results.Data, item => item.Price == 100);
            Assert.Contains(results.Data, item => item.Description == "Descrição test");
            Assert.Contains(results.Data, item => item.CardNumber == "************1234");
            Assert.Contains(results.Data, item => item.PaymentMethod == PaymentMethodEnum.DEBIT);
            Assert.Contains(results.Data, item => item.OwnerName == "Matheus Gustavo");
            Assert.Contains(results.Data, item => item.Cvv == 001);

        }
    }
}