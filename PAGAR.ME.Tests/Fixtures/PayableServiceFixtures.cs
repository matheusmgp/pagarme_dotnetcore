

using Application.Dtos;
using Application.Utils;
using Domain.Entities;

namespace PAGAR.ME.Tests.Fixtures
{
    public class PayableServiceFixtures
    {

        public static PayableDto PayableDtoFixture()
        {
            var payableDto = new PayableDto(100, DateTime.Now, PayableStatusEnum.PAID, PayableStatusEnum.AVAILABLE, 1);
            return payableDto;
        }
        public static PayableEntity PayableEntityFixture()
        {
            var props = new PayableEntityProps(100, DateTime.Now, PayableStatusEnum.PAID, PayableStatusEnum.AVAILABLE, 001);
            var payableTransaction = PayableEntity.CreateEntity(props);
            return payableTransaction;
        }
        public static ICollection<PayableEntity> PayableEntityListFixture(string method)
        {
            var items = new List<PayableEntity>
            {
                PayableEntity.CreateEntity(new PayableEntityProps(10, DateTime.Now, method, method == "paid" ? PayableStatusEnum.AVAILABLE :PayableStatusEnum.WAITING_FUNDS, 1)),
                PayableEntity.CreateEntity(new PayableEntityProps(10, DateTime.Now, method, method == "paid" ? PayableStatusEnum.AVAILABLE :PayableStatusEnum.WAITING_FUNDS, 1)),
                PayableEntity.CreateEntity(new PayableEntityProps(10, DateTime.Now, method, method == "paid" ? PayableStatusEnum.AVAILABLE :PayableStatusEnum.WAITING_FUNDS, 1))
            };
            return items;
        }
    }
}