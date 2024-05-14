﻿// <auto-generated />
using System;
using EShop.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EShop.Infrastucture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EShop.Core.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ce284d5a-1eba-4967-8d51-1d541a8025d2"),
                            Description = "Test",
                            Name = "Men Shirt"
                        },
                        new
                        {
                            Id = new Guid("91052f64-008c-4f54-a344-154d3f1ce37a"),
                            Description = "Test",
                            Name = "Men Pant"
                        },
                        new
                        {
                            Id = new Guid("09d05933-a638-4694-8c1d-f3e5c998124c"),
                            Description = "Test",
                            Name = "Accessory"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}