using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{

	public class categoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
            builder.ToTable("Categories");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();
            builder.Property(e => e.name)
				   .IsRequired()
				   .HasMaxLength(50);
			builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
		}
	}
}
