using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{
    public class productCheckoutConfiguration : IEntityTypeConfiguration<ProductCheckout>
    {
        public void Configure(EntityTypeBuilder<ProductCheckout> builder)
        {
            builder.ToTable("ProductCheckout");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();
            builder.Property(e => e.totalPayment).HasPrecision(18, 2);
            builder.Property(e => e.userID)
                    .IsRequired();
           
            builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
            builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
        }
    }
}
