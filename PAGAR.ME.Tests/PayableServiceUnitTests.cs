using Application.Services;
using Application.Services.Contracts;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.VisualBasic;
using Moq;
using System.Transactions;

namespace PAGAR.ME.Tests
{
    public class PayableServiceTests
    {
        private readonly IPayableRepository _payableRepository;
        private readonly IMapper _mapper;
        public readonly PayableService _service;

        public PayableServiceTests()
        {
            _service = new PayableService(_payableRepository, _mapper);
        }
        [Fact]
        public void EXPECTED_NUMBER_TOBE_ROUNDED()
        {                 
            var _number = _service.RoundNumber(100.5566);
            Assert.Equal(100.56, _number);

            var _number2 = _service.RoundNumber(100);
            Assert.Equal(100, _number2);

            var _number3 = _service.RoundNumber(1500.4589);
            Assert.Equal(1500,46, _number3);
        }

        [Fact]
        public void EXPECTED_ARRAY_TOBE_REDUCED()
        {
            ICollection<PayableEntity> items = new List<PayableEntity>();
            items.Add(PayableEntity.CreateEntity(10, DateTime.Now, "paid", "available", 1));
            items.Add(PayableEntity.CreateEntity(10, DateTime.Now, "paid", "available", 1));
            items.Add(PayableEntity.CreateEntity(10, DateTime.Now, "paid", "available", 1));

            var reduced = _service.Reduce(items);
            Assert.Equal(30,reduced);
        }

    }
}