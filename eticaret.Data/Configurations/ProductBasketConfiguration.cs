using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{
    public class ProductBasketConfiguration : IEntityTypeConfiguration<ProductBasket>
    {
        public void Configure(EntityTypeBuilder<ProductBasket> builder)
        {
            builder.ToTable("ProductBasket");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();
            builder.Property(e => e.price).HasPrecision(18, 2);
            builder.Property(e => e.stock).HasPrecision(18, 2);
            builder.Property(e => e.shippingAmount).HasPrecision(18, 2);
            builder.Property(e => e.ProductCheckoutID)
                    .IsRequired();
           
            builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
            builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
        }
    }
}
