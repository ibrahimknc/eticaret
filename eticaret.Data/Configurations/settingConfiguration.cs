using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{

	public class settingConfiguration : IEntityTypeConfiguration<Setting>
	{
		public void Configure(EntityTypeBuilder<Setting> builder)
		{
            builder.ToTable("Settings");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();

            builder.Property(e => e.address).HasMaxLength(250);

			builder.Property(e => e.email).HasMaxLength(50);

			builder.Property(e => e.keywords).HasMaxLength(500);

			builder.Property(e => e.phone).HasMaxLength(13);

			builder.Property(e => e.title)
				.IsRequired()
				.HasMaxLength(250);

			builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
		}
	}
}
