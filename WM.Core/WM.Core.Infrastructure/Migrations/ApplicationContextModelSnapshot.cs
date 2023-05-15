﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WM.Core.Infrastructure.Persistence;

#nullable disable

namespace WM.Core.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WM.Core.Domain.Entities.Facility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Manager")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Worker")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Admin = true,
                            Link = "/inventory",
                            Manager = true,
                            Name = "Inventory",
                            Worker = true
                        },
                        new
                        {
                            Id = 2,
                            Admin = true,
                            Link = "/orders",
                            Manager = true,
                            Name = "Orders",
                            Worker = false
                        },
                        new
                        {
                            Id = 3,
                            Admin = true,
                            Link = "/products",
                            Manager = true,
                            Name = "Products",
                            Worker = true
                        },
                        new
                        {
                            Id = 4,
                            Admin = true,
                            Link = "/warehouses",
                            Manager = true,
                            Name = "Warehouses",
                            Worker = false
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacilityId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = 0,
                            Email = "maxslag74@gmail.com",
                            IsDeleted = false,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedBy = 0,
                            Name = "Maxim",
                            Password = "Pa$$word1234",
                            Position = "Head Admin",
                            Role = 0,
                            Surname = "Babyuk"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = 0,
                            Email = "manager_test@gmail.com",
                            FacilityId = 3,
                            IsDeleted = false,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedBy = 0,
                            Name = "Manager",
                            Password = "Pa$$word1234",
                            Position = "Manager",
                            Role = 1,
                            Surname = "Test"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = 0,
                            Email = "worker_test@gmail.com",
                            FacilityId = 3,
                            IsDeleted = false,
                            ModifiedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedBy = 0,
                            Name = "Worker",
                            Password = "Pa$$word1234",
                            Position = "Worker",
                            Role = 2,
                            Surname = "Test"
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Inventory", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.Product", "Product")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WM.Core.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany("Inventories")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.User", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.Facility", "Facility")
                        .WithMany("Users")
                        .HasForeignKey("FacilityId");

                    b.Navigation("Facility");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Warehouse", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.Facility", "Facility")
                        .WithMany("Warehouses")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Facility", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("Warehouses");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Product", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Warehouse", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
