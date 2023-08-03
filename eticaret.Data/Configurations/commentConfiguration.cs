using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{
    public class commentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();
            builder.Property(e => e.detail)
                   .IsRequired();
            builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
            builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
        }
    }
}
