using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class eticaretContext : DbContext
    {
        public eticaretContext()
        {
        }

        public eticaretContext(DbContextOptions<eticaretContext> options)
            : base(options)
        {
        }

        public virtual DbSet<bulletin> bulletins { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<log> logs { get; set; }
        public virtual DbSet<logType> logTypes { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<productsIMG> productsIMGs { get; set; }
        public virtual DbSet<setting> settings { get; set; }
        public virtual DbSet<slider> sliders { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<userFavorite> userFavorites { get; set; }
        public virtual DbSet<viewsCategory> viewsCategories { get; set; }
        public virtual DbSet<viewsFavorite> viewsFavorites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=eticaret;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<bulletin>(entity =>
            {
                entity.ToTable("bulletin");

                entity.Property(e => e.creatingDate).HasColumnType("smalldatetime");

                entity.Property(e => e.email).HasMaxLength(50);
            });

            modelBuilder.Entity<category>(entity =>
            {
                entity.Property(e => e.name).HasMaxLength(50);
            });

            modelBuilder.Entity<log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.date).HasColumnType("smalldatetime");

                entity.Property(e => e.ip).HasMaxLength(50);
            });

            modelBuilder.Entity<logType>(entity =>
            {
                entity.ToTable("logType");

                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.note).HasMaxLength(50);
            });

            modelBuilder.Entity<product>(entity =>
            {
                entity.Property(e => e.basePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.creatingDate).HasColumnType("smalldatetime");

                entity.Property(e => e.image).HasMaxLength(500);

                entity.Property(e => e.name).HasMaxLength(50);

                entity.Property(e => e.salePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.stock).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.tags).HasMaxLength(50);
            });

            modelBuilder.Entity<productsIMG>(entity =>
            {
                entity.ToTable("productsIMG");

                entity.Property(e => e.url).HasMaxLength(500);
            });

            modelBuilder.Entity<setting>(entity =>
            {
                entity.Property(e => e.address).HasMaxLength(250);

                entity.Property(e => e.description).HasMaxLength(4000);

                entity.Property(e => e.email).HasMaxLength(50);

                entity.Property(e => e.keywords).HasMaxLength(500);

                entity.Property(e => e.phone)
                    .HasMaxLength(13)
                    .IsFixedLength(true);

                entity.Property(e => e.title).HasMaxLength(250);
            });

            modelBuilder.Entity<slider>(entity =>
            {
                entity.Property(e => e.image).HasMaxLength(500);

                entity.Property(e => e.title).HasMaxLength(50);
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.Property(e => e.creatingDate).HasColumnType("smalldatetime");

                entity.Property(e => e.email).HasMaxLength(50);

                entity.Property(e => e.firstName).HasMaxLength(50);

                entity.Property(e => e.lastLoginDate).HasColumnType("smalldatetime");

                entity.Property(e => e.lastName).HasMaxLength(50);

                entity.Property(e => e.password).HasMaxLength(32);
            });

            modelBuilder.Entity<userFavorite>(entity =>
            {
                entity.Property(e => e.creatingDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<viewsCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewsCategories");

                entity.Property(e => e.name).HasMaxLength(50);
            });

            modelBuilder.Entity<viewsFavorite>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewsFavorite");

                entity.Property(e => e.basePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.categoriName).HasMaxLength(50);

                entity.Property(e => e.creatingDate).HasColumnType("smalldatetime");

                entity.Property(e => e.image).HasMaxLength(500);

                entity.Property(e => e.name).HasMaxLength(50);

                entity.Property(e => e.salePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.stock).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.tags).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
