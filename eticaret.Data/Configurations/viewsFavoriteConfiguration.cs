using eticaret.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{

	public class viewsFavoriteConfiguration : IEntityTypeConfiguration<ViewsFavorite>
	{
		public void Configure(EntityTypeBuilder<ViewsFavorite> builder)
		{
			builder.HasNoKey();

			builder.ToTable("ViewsFavorite");

			builder.Property(e => e.basePrice).HasPrecision(18, 2);

			builder.Property(e => e.categoryName).HasMaxLength(50);

			builder.Property(e => e.creatingDate).HasColumnType("timestamp with time zone");

			builder.Property(e => e.image).HasMaxLength(50);

			builder.Property(e => e.name).HasMaxLength(50);

			builder.Property(e => e.salePrice).HasPrecision(18, 2);

			builder.Property(e => e.stock).HasPrecision(18, 2);

			builder.Property(e => e.tags).HasMaxLength(50);
		}
	}
}
