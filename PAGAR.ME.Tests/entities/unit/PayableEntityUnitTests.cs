

using AutoFixture;
using Domain.Entities;
using Domain.Validations;
using PAGAR.ME.Tests.Fixtures;

namespace PAGAR.ME.Tests
{
    public class PayableEntityUnitTests
    {
        [Fact]
        public void PayableEntity_CreateEntity_Return_Instance()
        {
            var entity = PayableServiceFixtures.PayableEntityFixture();
            Assert.Equal(100, entity.Amount);
            Assert.Equal("paid", entity.Status);
            Assert.Equal("available", entity.Availability);
            Assert.Equal(1, entity.TransactionId);

        }

        [Fact]
        public void PayableEntity_Validate_Method_Exception_Status()
        {
            var props = new Fixture().Create<PayableEntityProps>();
            props.Status = "";

            var exceptionResult = Assert.Throws<DomainValidationException>(() => PayableEntity.CreateEntity(props));
            Assert.Equal("Status deve ser informado", exceptionResult.Message);


        }
        [Fact]
        public void PayableEntity_Validate_Method_Exception_Availability()
        {
            var props = new Fixture().Create<PayableEntityProps>();
            props.Availability = "";
            var exceptionResult = Assert.Throws<DomainValidationException>(() => PayableEntity.CreateEntity(props));
            Assert.Equal("Availability deve ser informado", exceptionResult.Message);
        }
        [Fact]
        public void PayableEntity_Validate_Method_Exception_TransactionId()
        {
            var props = new Fixture().Create<PayableEntityProps>();
            props.TransactionId = 0;
            var exceptionResult = Assert.Throws<DomainValidationException>(() => PayableEntity.CreateEntity(props));
            Assert.Equal("TransactionId deve ser informado", exceptionResult.Message);
        }

    }
}