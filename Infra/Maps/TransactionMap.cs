

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.Maps
{
    public class TransactionMap : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transaction");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();
            builder.Property(x => x.Price)
              .HasColumnName("price");
            builder.Property(x => x.Description)
              .HasColumnName("description");
            builder.Property(x => x.PaymentMethod)
             .HasColumnName("payment_method");
            builder.Property(x => x.CardNumber)
             .HasColumnName("card_number");
            builder.Property(x => x.OwnerName)
             .HasColumnName("owner_name");
            builder.Property(x => x.CardExpiresDate)
              .HasColumnName("card_expires_date");
            builder.Property(x => x.Cvv)
             .HasColumnName("cvv");
          


        }
    }
}
