using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class dbeticaretContext : DbContext
    {
        public dbeticaretContext()
        {
        }

        public dbeticaretContext(DbContextOptions<dbeticaretContext> options)
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
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=dbeticaret;User Id=postgres;Password=Vffkvs621;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_Turkey.1252");

            modelBuilder.Entity<bulletin>(entity =>
            {
                entity.ToTable("bulletin");

                entity.Property(e => e.creatingDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<category>(entity =>
            {
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.date).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ip)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<logType>(entity =>
            {
                entity.ToTable("logType");

                entity.Property(e => e.id).HasDefaultValueSql("nextval('logtype_id_seq'::regclass)");

                entity.Property(e => e.note).HasMaxLength(50);
            });

            modelBuilder.Entity<product>(entity =>
            {
                entity.Property(e => e.basePrice).HasPrecision(18, 2);

                entity.Property(e => e.creatingDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.image)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.salePrice).HasPrecision(18, 2);

                entity.Property(e => e.stock).HasPrecision(18, 2);

                entity.Property(e => e.tags).HasMaxLength(50);
            });

            modelBuilder.Entity<productsIMG>(entity =>
            {
                entity.ToTable("productsIMG");

                entity.Property(e => e.id).HasDefaultValueSql("nextval('productsimg_id_seq'::regclass)");

                entity.Property(e => e.url)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<setting>(entity =>
            {
                entity.Property(e => e.address).HasMaxLength(250);

                entity.Property(e => e.email).HasMaxLength(50);

                entity.Property(e => e.keywords).HasMaxLength(500);

                entity.Property(e => e.phone).HasMaxLength(13);

                entity.Property(e => e.title)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<slider>(entity =>
            {
                entity.Property(e => e.image)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.Property(e => e.creatingDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.firstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.lastLoginDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.lastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<userFavorite>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('userfavorites_id_seq'::regclass)");

                entity.Property(e => e.creatingDate).HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<viewsCategory>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.name).HasMaxLength(50);
            });

            modelBuilder.Entity<viewsFavorite>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("viewsFavorite");

                entity.Property(e => e.basePrice).HasPrecision(18, 2);

                entity.Property(e => e.categoryName).HasMaxLength(50);

                entity.Property(e => e.creatingDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.image).HasMaxLength(50);

                entity.Property(e => e.name).HasMaxLength(50);

                entity.Property(e => e.salePrice).HasPrecision(18, 2);

                entity.Property(e => e.stock).HasPrecision(18, 2);

                entity.Property(e => e.tags).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
