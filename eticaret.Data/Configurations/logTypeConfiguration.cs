using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eticaret.Data.Configurations
{

	public class logTypeConfiguration : IEntityTypeConfiguration<LogType>
	{
		public void Configure(EntityTypeBuilder<LogType> builder)
		{
			builder.ToTable("LogType");  
			builder.Property(e => e.note).HasMaxLength(50); 
		}
	}
}
