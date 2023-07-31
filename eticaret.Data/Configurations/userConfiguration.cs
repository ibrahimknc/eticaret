using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{

	public class userConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
            builder.ToTable("Users");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();

            builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");

			builder.Property(e => e.email)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.firstName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.lastLoginDate).HasColumnType("timestamp with time zone");

			builder.Property(e => e.lastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.password)
				.IsRequired()
				.HasMaxLength(32);
		}
	}
}
