using eticaret.Data.Configurations;
using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace eticaret.Data
{
	public class dbeticaretContext : DbContext
	{ 
 
		public dbeticaretContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{ 
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(dbeticaretContext).Assembly);
			modelBuilder.HasDefaultSchema("EticaretSchemas");
			modelBuilder.ApplyConfiguration(new bulletinConfiguration());
			modelBuilder.ApplyConfiguration(new categoryConfiguration());
			modelBuilder.ApplyConfiguration(new logConfiguration());
			modelBuilder.ApplyConfiguration(new logTypeConfiguration());
			modelBuilder.ApplyConfiguration(new productConfiguration());
			modelBuilder.ApplyConfiguration(new productIMGConfiguration());
			modelBuilder.ApplyConfiguration(new settingConfiguration());
			modelBuilder.ApplyConfiguration(new sliderConfiguration());
			modelBuilder.ApplyConfiguration(new userConfiguration());
			modelBuilder.ApplyConfiguration(new userFavoriteConfiguration());
		}
 

		public DbSet<Bulletin> bulletins { get; set; }
		public DbSet<Category> categories { get; set; }
		public DbSet<Log> logs { get; set; }
		public DbSet<LogType> logTypes { get; set; }
		public DbSet<Product> products { get; set; }
		public DbSet<ProductIMG> productsIMGs { get; set; }
		public DbSet<Setting> settings { get; set; }
		public DbSet<Slider> sliders { get; set; }
		public DbSet<User> users { get; set; }
		public DbSet<UserFavorite> userFavorites { get; set; }
		
	}
}
