using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{

	public class userFavoriteConfiguration : IEntityTypeConfiguration<UserFavorite>
	{
		public void Configure(EntityTypeBuilder<UserFavorite> builder)
		{  
			builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");

		}
	}
}
