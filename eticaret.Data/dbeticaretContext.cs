using eticaret.Data.Configurations;
using eticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.ApplyConfiguration(new commentConfiguration());
            modelBuilder.ApplyConfiguration(new productViewsConfiguration());
            modelBuilder.ApplyConfiguration(new shopConfiguration());

            #region Log ForeignKey
            modelBuilder.Entity<Log>()
          .HasOne(l => l.LogType)
          .WithMany()
          .HasForeignKey(l => l.type);
            #endregion

            #region ProductIMG ForeignKey
            modelBuilder.Entity<ProductIMG>()
          .HasOne(l => l.Product)
          .WithMany()
          .HasForeignKey(l => l.productID);
            #endregion

            #region ProductViews ForeignKey
            modelBuilder.Entity<ProductViews>()
          .HasOne(l => l.Product)
          .WithMany()
          .HasForeignKey(l => l.productID);
            #endregion

            #region UserFavorite ForeignKey
            modelBuilder.Entity<UserFavorite>()
          .HasOne(l => l.User)
          .WithMany()
          .HasForeignKey(l => l.userID);

            modelBuilder.Entity<UserFavorite>()
          .HasOne(l => l.Product)
          .WithMany()
          .HasForeignKey(l => l.productID);
            #endregion

            #region Product ForeignKey
            modelBuilder.Entity<Product>()
            .HasOne(l => l.Category)
            .WithMany()
            .HasForeignKey(l => l.categoriID);

            modelBuilder.Entity<Product>()
            .HasOne(l => l.Shop)
            .WithMany()
            .HasForeignKey(l => l.shopID);
            #endregion

            #region Comments ForeignKey
            modelBuilder.Entity<Comment>()
        .HasOne(l => l.User)
        .WithMany()
        .HasForeignKey(l => l.userID);

            modelBuilder.Entity<Comment>()
          .HasOne(l => l.Product)
          .WithMany()
          .HasForeignKey(l => l.productID);
            #endregion
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
        public DbSet<Comment> comments { get; set; }
        public DbSet<ProductViews> productViews { get; set; }
        public DbSet<Shop> shops { get; set; }

    }
}
