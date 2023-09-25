using System.Collections.ObjectModel;
using Application.Dtos;
using Application.Mappers;
using Application.Services;
using Application.Services.Contracts;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Moq;

namespace PAGAR.ME.Tests
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
        private readonly Mock<IPayableService> _payableService = new Mock<IPayableService>();
       
        private readonly IMapper _map;
        public readonly TransactionService sut;

        public TransactionServiceTests()
        {
            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ConfigureAutoMapper());
            });
            _map = config.CreateMapper();
            sut = new TransactionService(_transactionRepository.Object, _payableService.Object, _map);
        }
        [Fact]
        public void EXPECTED_STATUS_TOBE_EQUALS_PAID()
        {
            var status = sut.SetStatus("debit_card");
            Assert.Equal("paid", status);
        }

        [Fact]
        public void EXPECTED_STATUS_TOBE_EQUALS_WAITING_FUNDS()
        {
            var status = sut.SetStatus("credit_card");
            Assert.Equal("waiting_funds", status);
        }

        [Fact]
        public void EXPECTED_PRICE_TOBE_EQUALS_97()
        {
            var price = sut.CalculateFee("debit_card", 100);
            Assert.Equal(97, price);
        }
        [Fact]
        public void EXPECTED_PRICE_TOBE_EQUALS_95()
        {
            var price = sut.CalculateFee("credit_card", 100);
            Assert.Equal(95, price);
        }

        [Fact]
        public void EXPECTED_AVAILABILITY_TOBE_EQUALS_AVAILABLE()
        {
            var availability = sut.SetAvailability("debit_card");
            Assert.Equal("available", availability);
        }
        [Fact]
        public void EXPECTED_AVAILABILITY_TOBE_EQUALS_WAITING_FUNDS()
        {
            var availability = sut.SetAvailability("credit_card");
            Assert.Equal("waiting_funds", availability);
        }

        [Fact]
        public async void SHOULD_CREATE_TRANSACTION()
        {
            var transactionDto = new TransactionDto(100, "description", "debit_card", "1234567894561234", "Matheus Gustavo", DateTime.Now, 884);
            var transactionEntity = TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "description", "debit_card", "1234567894561234", "Matheus Gustavo", 884));
            transactionEntity.Id = 10;
            var payableDto = new PayableDto(100, DateTime.Now, "paid", "available", transactionEntity.Id);            
         
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
        public async void SHOULD_RETRIEVE_ALL_TRANSACTIONS()
        {                 
            var transactions = new Collection<TransactionEntity>
            {
              TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "description", "debit_card", "1234567894561234", "Matheus Gustavo", 884)),
              TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "description", "debit_card", "1234567894561234", "Matheus Gustavo", 884)),
              TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "description", "debit_card", "1234567894561234", "Matheus Gustavo", 884))
            };
           
            _transactionRepository.Setup(x => x.GetAll()).ReturnsAsync(transactions);           

            var results = await sut.GetAll();
            Assert.Contains(results.Data, item => item.Price == 100);
            Assert.Contains(results.Data, item => item.Description == "description");
            Assert.Contains(results.Data, item => item.CardNumber == "************1234");
            Assert.Contains(results.Data, item => item.PaymentMethod == "debit_card");
            Assert.Contains(results.Data, item => item.OwnerName == "Matheus Gustavo");
            Assert.Contains(results.Data, item => item.Cvv == 884);
           
        }
    }
}