

using Application.Utils;
using AutoFixture;
using Domain.Entities;
using Domain.Validations;
using NuGet.Frameworks;
using PAGAR.ME.Tests.Fixtures;

namespace PAGAR.ME.Tests
{
   public class TransactionEntityUnitTests
   {
      [Fact]
      public void TransactionEntity_CreateEntity_Return_Instance()
      {
         var entity = TransactionServiceFixtures.TransactionEntityFixture();
         Assert.Equal(100, entity.Price);
         Assert.Equal("Descrição test", entity.Description);
         Assert.Equal(PaymentMethodEnum.DEBIT, entity.PaymentMethod);
         Assert.Equal("Matheus", entity.OwnerName);
         Assert.Equal(001, entity.Cvv);
         Assert.Equal("************1234", entity.CardNumber);
      }

      [Fact]
      public void TransactionEntity_Masked_Return_Masked_CardNumber()
      {

         var cardNumber = "1234567891478569";

         var entity = TransactionEntity.Masked(cardNumber);

         Assert.Equal("************8569", entity);

      }

      [Fact]
      public void TransactionEntity_Validate_Method_Exception_Description()
      {
         var props = new Fixture().Create<TransactionEntityProps>();
         props.PaymentMethod = "debit_card";
         props.CardNumber = "1234567891478569";
         props.Description = "";

         var exceptionResult = Assert.Throws<DomainValidationException>(() => TransactionEntity.CreateEntity(props));
         Assert.Equal("Description deve ser informado", exceptionResult.Message);


      }
      [Fact]
      public void TransactionEntity_Validate_Method_Exception_PaymentMethod()
      {
         var props = new Fixture().Create<TransactionEntityProps>();
         props.CardNumber = "1234567891478569";
         var exceptionResult = Assert.Throws<DomainValidationException>(() => TransactionEntity.CreateEntity(props));
         Assert.Equal("PaymentMethod deve debit_card ou credit_card", exceptionResult.Message);
      }
      [Fact]
      public void TransactionEntity_Validate_Method_Exception_CardNumber()
      {
         var props = new Fixture().Create<TransactionEntityProps>();
         props.PaymentMethod = "debit_card";
         var exceptionResult = Assert.Throws<DomainValidationException>(() => TransactionEntity.CreateEntity(props));
         Assert.Equal("CardNumber deve ter 16 caracteres", exceptionResult.Message);
      }
      [Fact]
      public void TransactionEntity_Validate_Method_Exception_OwnerName()
      {
         var props = new Fixture().Create<TransactionEntityProps>();
         props.PaymentMethod = "debit_card";
         props.CardNumber = "1234567891478569";
         props.OwnerName = "";
         var exceptionResult = Assert.Throws<DomainValidationException>(() => TransactionEntity.CreateEntity(props));
         Assert.Equal("OwnerName deve ser informado", exceptionResult.Message);
      }
      [Fact]
      public void TransactionEntity_Validate_Method_Exception_Cvv()
      {
         var props = new Fixture().Create<TransactionEntityProps>();
         props.PaymentMethod = "debit_card";
         props.CardNumber = "1234567891478569";
         props.Cvv = 0;
         var exceptionResult = Assert.Throws<DomainValidationException>(() => TransactionEntity.CreateEntity(props));
         Assert.Equal("CVV deve ser informado", exceptionResult.Message);
      }
   }
}