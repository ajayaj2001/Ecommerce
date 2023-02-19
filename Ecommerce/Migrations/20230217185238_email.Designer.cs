﻿// <auto-generated />
using System;
using Customer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Customer.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20230217185238_email")]
    partial class email
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("7cf56f52-1aab-4646-b090-d337aac18370"),
                            City = "dindigul",
                            Country = "India",
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            IsActive = true,
                            Line1 = "psna college",
                            Line2 = "psna nagar",
                            PhoneNumber = "1234567890",
                            StateName = "tamilnadu",
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
                            Id = new Guid("dbe63d8c-7a58-4c10-a68f-4e7a903ef5a8"),
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
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
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

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("credentials");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2162c878-9a7e-4316-a5d4-ecfc3651c8b0"),
                            CreatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            EmailAddress = "tester@gmail.com",
                            IsActive = true,
                            Password = "tester2001",
                            Role = "admin",
                            UpdatedBy = new Guid("00000000-0000-0000-0000-000000000000"),
                            UserId = new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e")
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
