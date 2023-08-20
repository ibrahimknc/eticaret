using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{
    internal class userAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddress");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();
            builder.Property(e => e.firstName)
                  .IsRequired()
                  .HasMaxLength(50);
            builder.Property(e => e.lastName)
                  .IsRequired()
                  .HasMaxLength(50);
            builder.Property(e => e.title)
                  .IsRequired()
                  .HasMaxLength(50);
            builder.Property(e => e.country)
                  .IsRequired()
                  .HasMaxLength(50);
            builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
            builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
        }
    }
}
