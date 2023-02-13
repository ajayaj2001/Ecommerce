﻿// <auto-generated />
using System;
using Customer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Customer.Migrations
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Customer.Entities.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Zipcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cbdbfada-04e8-479f-b71c-dfd9b15bf146"),
                            City = "chennai",
                            Country = "tamil nadu",
                            CreatedAt = "01-01-0001 12:00:00 AM",
                            CreatedBy = new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                            IsActive = true,
                            Line1 = "anna nagar",
                            Line2 = "velachery",
                            PhoneNumber = "1234567890",
                            StateName = "tamil nadu",
                            Type = "personal",
                            UpdatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                            Zipcode = "626101"
                        });
                });

            modelBuilder.Entity("Customer.Entities.Models.CardDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CVVNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExpiryDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b265a47d-41fb-48dd-a5a9-de9fd6715695"),
                            CVVNo = "233",
                            CardNumber = "1234 5678 90",
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            ExpiryDate = "24/23",
                            HolderName = "tester",
                            IsActive = true,
                            Type = "personal",
                            UpdatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e")
                        });
                });

            modelBuilder.Entity("Customer.Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                            CreatedAt = "01-01-0001 12:00:00 AM",
                            CreatedBy = new Guid("4cbdbef2-9410-4dbe-93ef-940520e30c6d"),
                            EmailAddress = "tester@gmail.com",
                            FirstName = "tester",
                            IsActive = true,
                            LastName = "here",
                            UpdatedBy = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("Customer.Entities.Models.UserCredential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("credentials");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f0957a73-e62a-440e-9914-d089914bbfaa"),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsActive = true,
                            Password = "tester2001",
                            Role = "admin",
                            UpdatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                            UserName = "tester"
                        });
                });

            modelBuilder.Entity("Customer.Entities.Models.Address", b =>
                {
                    b.HasOne("Customer.Entities.Models.User", null)
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Customer.Entities.Models.CardDetail", b =>
                {
                    b.HasOne("Customer.Entities.Models.User", null)
                        .WithMany("CardDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Customer.Entities.Models.UserCredential", b =>
                {
                    b.HasOne("Customer.Entities.Models.User", null)
                        .WithOne("Credentials")
                        .HasForeignKey("Customer.Entities.Models.UserCredential", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}