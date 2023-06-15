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

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facilities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Earth, Europe, Ukraine",
                            Description = "This is the main facility.",
                            IsActive = true,
                            Name = "Main Facility"
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

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

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Link = "/dashboard",
                            Name = "Dashboard"
                        },
                        new
                        {
                            Id = 2,
                            Link = "/inventory",
                            Name = "Inventory"
                        },
                        new
                        {
                            Id = 3,
                            Link = "/orders",
                            Name = "Orders"
                        },
                        new
                        {
                            Id = 4,
                            Link = "/products",
                            Name = "Products"
                        },
                        new
                        {
                            Id = 5,
                            Link = "/warehouses",
                            Name = "Warehouses"
                        },
                        new
                        {
                            Id = 6,
                            Link = "/facilities",
                            Name = "Facilities"
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.MenuRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("MenuRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MenuId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            MenuId = 1,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            MenuId = 1,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 4,
                            MenuId = 2,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 5,
                            MenuId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 6,
                            MenuId = 2,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 7,
                            MenuId = 3,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 8,
                            MenuId = 3,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 9,
                            MenuId = 3,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 10,
                            MenuId = 4,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 11,
                            MenuId = 4,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 12,
                            MenuId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            Id = 13,
                            MenuId = 5,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 14,
                            MenuId = 6,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "New"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Closed"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Canceled"
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Worker"
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacilityId")
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

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "maxslag74@gmail.com",
                            FacilityId = 1,
                            Name = "Maxim",
                            Password = "Pa$$word1234",
                            Position = "Head Admin",
                            RoleId = 1,
                            Surname = "Babyuk"
                        },
                        new
                        {
                            Id = 2,
                            Email = "manager_test@gmail.com",
                            FacilityId = 1,
                            Name = "Manager",
                            Password = "Pa$$word1234",
                            Position = "Manager",
                            RoleId = 2,
                            Surname = "Test"
                        },
                        new
                        {
                            Id = 3,
                            Email = "worker_test@gmail.com",
                            FacilityId = 1,
                            Name = "Worker",
                            Password = "Pa$$word1234",
                            Position = "Worker",
                            RoleId = 3,
                            Surname = "Test"
                        });
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Warehouses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is a warehouse A1",
                            FacilityId = 1,
                            Name = "W-A1"
                        });
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

            modelBuilder.Entity("WM.Core.Domain.Entities.MenuRole", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.MenuItem", "MenuItem")
                        .WithMany("MenuRoles")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WM.Core.Domain.Entities.Role", "Role")
                        .WithMany("MenuRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Order", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.Facility", "Facility")
                        .WithMany("Orders")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WM.Core.Domain.Entities.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WM.Core.Domain.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.User", b =>
                {
                    b.HasOne("WM.Core.Domain.Entities.Facility", "Facility")
                        .WithMany("Users")
                        .HasForeignKey("FacilityId");

                    b.HasOne("WM.Core.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");

                    b.Navigation("Role");
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
                    b.Navigation("Orders");

                    b.Navigation("Users");

                    b.Navigation("Warehouses");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.MenuItem", b =>
                {
                    b.Navigation("MenuRoles");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Product", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Role", b =>
                {
                    b.Navigation("MenuRoles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("WM.Core.Domain.Entities.Warehouse", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
