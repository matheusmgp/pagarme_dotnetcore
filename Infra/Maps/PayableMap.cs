

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Maps
{
    public class PayableMap : IEntityTypeConfiguration<PayableEntity>
    {
        public void Configure(EntityTypeBuilder<PayableEntity> builder)
        {
            builder.ToTable("payable");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();
            builder.Property(x => x.Amount)
              .HasColumnName("amount");
            builder.Property(x => x.PaymentDate)
              .HasColumnName("payment_date");
            builder.Property(x => x.Status)
             .HasColumnName("status");
            builder.Property(x => x.Availability)
             .HasColumnName("availability");
            builder.Property(x => x.TransactionId)
             .HasColumnName("transaction_id");
            builder.HasOne(x => x.Transaction)
              
             .WithOne(e => e.Payable)
          .HasForeignKey<TransactionEntity>(p => p.Id);
        }
    }
}
