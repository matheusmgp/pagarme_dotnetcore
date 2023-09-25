using System.Collections.ObjectModel;
using Application.Mappers;
using Application.Services;
using Application.Utils;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Moq;

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
            ICollection<PayableEntity> items = new List<PayableEntity>
            {
                PayableEntity.CreateEntity(new PayableEntityProps(10, DateTime.Now, "paid", "available", 1)),
                PayableEntity.CreateEntity(new PayableEntityProps(10, DateTime.Now, "paid", "available", 1)),
                PayableEntity.CreateEntity(new PayableEntityProps(10, DateTime.Now, "paid", "available", 1))
            };

            var reduced = sut.Reduce(items);
            Assert.Equal(30, reduced);
        }

        [Fact]
        public async void PayableService_GetAll_Method()
        {
            var payables = new Collection<PayableEntity>
            {
              PayableEntity.CreateEntity(new PayableEntityProps(100, DateTime.Now,"paid", "available", 884)),
              PayableEntity.CreateEntity(new PayableEntityProps(100, DateTime.Now,"paid", "available", 884)),
            };

            _payableRepository.Setup(x => x.GetAll()).ReturnsAsync(payables);

            var results = await sut.GetAll();
            Assert.Contains(results.Data, item => item.Amount == 100);
            Assert.Contains(results.Data, item => item.Status == "paid");
            Assert.Contains(results.Data, item => item.Availability == "available");
            Assert.Contains(results.Data, item => item.TransactionId == 884);

        }
        [Fact]
        public async void PayableService_GetAllPayable_Method()
        {
            var availables = new Collection<PayableEntity>
            {
              PayableEntity.CreateEntity(new PayableEntityProps(1000, DateTime.Now,"paid", "available", 884)),
              PayableEntity.CreateEntity(new PayableEntityProps(100, DateTime.Now,"paid", "available", 884)),
            };
            var waiting = new Collection<PayableEntity>
            {
              PayableEntity.CreateEntity(new PayableEntityProps(500, DateTime.Now,"paid", "available", 884)),
              PayableEntity.CreateEntity(new PayableEntityProps(100, DateTime.Now,"waiting_funds", "waiting_funds", 884)),
            };


            _payableRepository.Setup(x => x.GetAllPayable(PayableStatusEnum.AVAILABLE)).ReturnsAsync(availables);
            _payableRepository.Setup(x => x.GetAllPayable(PayableStatusEnum.WAITING_FUNDS)).ReturnsAsync(waiting);

            var results = await sut.GetAllPayables();

            Assert.Equal(1100, results.Data.Availables);
            Assert.Equal(600, results.Data.WaitingFunds);

        }
    }
}