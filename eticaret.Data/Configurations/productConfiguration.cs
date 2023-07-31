using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{

	public class productConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
            builder.ToTable("Products");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();

            builder.Property(e => e.basePrice).HasPrecision(18, 2);

			builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");

			builder.Property(e => e.image)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.name)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.salePrice).HasPrecision(18, 2);

			builder.Property(e => e.stock).HasPrecision(18, 2);

			builder.Property(e => e.tags).HasMaxLength(50);

		}
	}
}
