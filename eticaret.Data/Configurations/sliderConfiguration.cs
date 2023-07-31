using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{

	public class sliderConfiguration : IEntityTypeConfiguration<Slider>
	{
		public void Configure(EntityTypeBuilder<Slider> builder)
		{
            builder.ToTable("Sliders");
            builder.Property(c => c.id)
           .HasDefaultValueSql("uuid_generate_v4()")
           .IsRequired();

            builder.Property(e => e.image)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.Property(e => e.title)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(e => e.updatedTime).HasColumnType("timestamp with time zone");
			builder.Property(e => e.creatingTime).HasColumnType("timestamp with time zone");
		}
	}
}
