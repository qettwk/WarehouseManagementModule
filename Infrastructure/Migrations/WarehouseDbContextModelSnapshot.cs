﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    partial class WarehouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AutomobileCategory", b =>
                {
                    b.Property<Guid>("AutomobilesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.HasKey("AutomobilesId", "CategoriesId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("AutomobileCategory", "WarehouseManagment");
                });

            modelBuilder.Entity("Domain.Automobile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Automobiles", "WarehouseManagment");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Discount")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", "WarehouseManagment");
                });

            modelBuilder.Entity("Domain.RecordWrite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AutomobileID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RecordWrites", "WarehouseManagment");
                });

            modelBuilder.Entity("AutomobileCategory", b =>
                {
                    b.HasOne("Domain.Automobile", null)
                        .WithMany()
                        .HasForeignKey("AutomobilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
