using System.Collections.ObjectModel;
using Application.Dtos;
using Application.Errors;
using Application.Services;
using Application.Services.Contracts;
using Application.Validations;
using AutoFixture;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Moq;
using Shouldly;

namespace PAGAR.ME.Tests
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
        private readonly Mock<IPayableService>  _payableService = new Mock<IPayableService>();
        private readonly  Mock<IMapper> _mapper = new Mock<IMapper>();
        public readonly   TransactionService sut;

        public TransactionServiceTests()
        {
            sut = new TransactionService(_transactionRepository.Object, _payableService.Object, _mapper.Object);
        }
        [Fact]
        public void EXPECTED_STATUS_TOBE_EQUALS_PAID()
        {                 
            var status = sut.SetStatus("debit_card");
            Assert.Equal( "paid", status);
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
            var price = sut.CalculateFee("debit_card",100);
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
           var transactionDto =  new TransactionDto(100,"description","debit_card","1234567894561234","dasdas",DateTime.Now,444);
           var transactionEntity =  TransactionEntity.CreateEntity(new TransactionEntityProps(100,DateTime.Now,"description","debit_card","1234567894561234","dasdas",444));


          
            
           _transactionRepository.Setup(x => x.Create(transactionEntity)).ReturnsAsync(transactionEntity);

            var payableDto = new PayableDto(100, DateTime.Now, "paid", "available", 1);
            
           _payableService.Setup(y => y.CreateAsync(payableDto));
           _mapper.Setup(m => m.Map< TransactionDto>(It.IsAny<TransactionEntity>())).Returns(transactionDto);
           
         
           var result = await  sut.CreateAsync(transactionDto);
           Assert.Equal(null,result);
        }
    }
}