
using System.Collections.ObjectModel;
using Application.Dtos;
using Application.Utils;
using AutoFixture;
using Domain.Entities;

namespace PAGAR.ME.Tests.Fixtures
{
    public class TransactionServiceFixtures
    {

        public static TransactionDto TransactionDtoFixture()
        {
            var transactionDto = new TransactionDto(100, "Descrição test", PaymentMethodEnum.DEBIT, "0000000000001234", "Matheus", DateTime.Now, 001);
            return transactionDto;
        }
        public static PayableDto PayableDtoFixture()
        {
            var payableDto = new PayableDto(100, DateTime.Now, PayableStatusEnum.PAID, PayableStatusEnum.AVAILABLE, 1);
            return payableDto;
        }
        public static TransactionEntity TransactionEntityFixture()
        {
            var props = new TransactionEntityProps(100, DateTime.Now, "Descrição test", PaymentMethodEnum.DEBIT, "0000000000001234", "Matheus", 001);
            var transactionEntity = TransactionEntity.CreateEntity(props);
            transactionEntity.Id = 10;
            return transactionEntity;
        }
        public static ICollection<TransactionEntity> TransactionEntityListFixture()
        {
            var transactions = new Collection<TransactionEntity>
            {
              TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "Descrição test", PaymentMethodEnum.DEBIT, "0000000000001234", "Matheus Gustavo", 001)),
              TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "Descrição test", PaymentMethodEnum.DEBIT, "0000000000001234", "Matheus Gustavo", 001)),
              TransactionEntity.CreateEntity(new TransactionEntityProps(100, DateTime.Now, "Descrição test", PaymentMethodEnum.DEBIT, "0000000000001234", "Matheus Gustavo", 001))
            };
            return transactions;
        }
    }
}