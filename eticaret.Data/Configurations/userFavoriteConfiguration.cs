using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{

	public class userFavoriteConfiguration : IEntityTypeConfiguration<UserFavorite>
	{
		public void Configure(EntityTypeBuilder<UserFavorite> builder)
		{
            builder.ToTable("UserFavorites");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();

            builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");

		}
	}
}
