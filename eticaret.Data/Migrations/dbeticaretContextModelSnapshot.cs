﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using eticaret.Data;

namespace eticaret.Data.Migrations
{
    [DbContext(typeof(dbeticaretContext))]
    partial class dbeticaretContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("EticaretSchemas")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("eticaret.Domain.Entities.Bulletin", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Bulletin");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("detail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("productID")
                        .HasColumnType("uuid");

                    b.Property<int>("rating")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("userID")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("productID");

                    b.HasIndex("userID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Log", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ip")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("note")
                        .HasColumnType("text");

                    b.Property<int>("type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("userID")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("type");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.LogType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("note")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("id");

                    b.ToTable("LogType");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<decimal?>("basePrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<Guid>("categoriID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("details")
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("popularity")
                        .HasColumnType("integer");

                    b.Property<decimal>("salePrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<Guid>("shopID")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("stock")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("tags")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("categoriID");

                    b.HasIndex("shopID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.ProductIMG", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("productID")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("id");

                    b.HasIndex("productID");

                    b.ToTable("ProductIMG");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.ProductViews", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ip")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("productID")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("productID");

                    b.ToTable("ProductViews");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Setting", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("address")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("keywords")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("phone")
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Shop", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Slider", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("rank")
                        .HasColumnType("integer");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("address")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("lastLoginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("phone")
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.UserFavorite", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("creatingTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("productID")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("updatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("userID")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("productID");

                    b.HasIndex("userID");

                    b.ToTable("UserFavorites");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Comment", b =>
                {
                    b.HasOne("eticaret.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eticaret.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Log", b =>
                {
                    b.HasOne("eticaret.Domain.Entities.LogType", "LogType")
                        .WithMany()
                        .HasForeignKey("type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogType");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.Product", b =>
                {
                    b.HasOne("eticaret.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("categoriID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eticaret.Domain.Entities.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("shopID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.ProductIMG", b =>
                {
                    b.HasOne("eticaret.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.ProductViews", b =>
                {
                    b.HasOne("eticaret.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eticaret.Domain.Entities.UserFavorite", b =>
                {
                    b.HasOne("eticaret.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eticaret.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
