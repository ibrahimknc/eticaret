using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{
	public class bulletinConfiguration : IEntityTypeConfiguration<Bulletin>
	{
		public void Configure(EntityTypeBuilder<Bulletin> builder)
		{
			builder.ToTable("Bulletin"); 
            builder.Property(e => e.email)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
		}
	}
}
