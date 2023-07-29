using eticaret.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace eticaret.Data.Configurations
{

	public class viewsCategoryConfiguration : IEntityTypeConfiguration<ViewsCategory>
	{
		public void Configure(EntityTypeBuilder<ViewsCategory> builder)
		{
			builder.HasNoKey(); 
			builder.Property(e => e.name).HasMaxLength(50);
		}
	}
}
