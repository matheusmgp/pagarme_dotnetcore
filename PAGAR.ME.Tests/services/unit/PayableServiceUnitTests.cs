using System.Collections.ObjectModel;
using Application.Mappers;
using Application.Services;
using Application.Utils;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Moq;
using PAGAR.ME.Tests.Fixtures;

namespace PAGAR.ME.Tests
{
    public class PayableServiceUnitTests
    {
        private readonly Mock<IPayableRepository> _payableRepository = new Mock<IPayableRepository>();
        public readonly PayableService sut;
        private readonly IMapper _map;

        public PayableServiceUnitTests()
        {
            var config = new MapperConfiguration(opt =>
           {
               opt.AddProfile(new ConfigureAutoMapper());
           });
            _map = config.CreateMapper();
            sut = new PayableService(_payableRepository.Object, _map);
        }
        [Fact]
        public void PayableService_RoundNumber_Method()
        {
            var _number = sut.RoundNumber(100.5566);
            Assert.Equal(100.56, _number);

            var _number2 = sut.RoundNumber(100);
            Assert.Equal(100, _number2);

            var _number3 = sut.RoundNumber(1500.4589);
            Assert.Equal(1500, 46, _number3);
        }

        [Fact]
        public void PayableService_Reduce_Method()
        {
            var items = PayableServiceFixtures.PayableEntityListFixture(PayableStatusEnum.PAID);

            var reduced = sut.Reduce(items);
            Assert.Equal(30, reduced);
        }

        [Fact]
        public async void PayableService_GetAll_Method()
        {
            var payables = PayableServiceFixtures.PayableEntityListFixture(PayableStatusEnum.PAID);

            _payableRepository.Setup(x => x.GetAll()).ReturnsAsync(payables);

            var results = await sut.GetAll();
            Assert.Contains(results.Data, item => item.Amount == 10);
            Assert.Contains(results.Data, item => item.Status == PayableStatusEnum.PAID);
            Assert.Contains(results.Data, item => item.Availability == PayableStatusEnum.AVAILABLE);
            Assert.Contains(results.Data, item => item.TransactionId == 1);

        }
        [Fact]
        public async void PayableService_GetAllPayable_Method()
        {
            var availables = PayableServiceFixtures.PayableEntityListFixture(PayableStatusEnum.PAID);
            var waiting = PayableServiceFixtures.PayableEntityListFixture(PayableStatusEnum.WAITING_FUNDS);


            _payableRepository.Setup(x => x.GetAllPayable(PayableStatusEnum.AVAILABLE)).ReturnsAsync(availables);
            _payableRepository.Setup(x => x.GetAllPayable(PayableStatusEnum.WAITING_FUNDS)).ReturnsAsync(waiting);

            var results = await sut.GetAllPayables();

            Assert.Equal(30, results.Data.Availables);
            Assert.Equal(30, results.Data.WaitingFunds);

        }
    }
}