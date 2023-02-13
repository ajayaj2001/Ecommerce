using Microsoft.EntityFrameworkCore;
using System;
using Customer.Entities.Models;

namespace Customer.DbContexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CardDetail> Cards { get; set; }
        public DbSet<UserCredential> credentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //string CustomerPath = @"F:\work\project\training\Address Book\Customer\DbContext\data\Customer.csv";
            //string[] userValues = File.ReadAllText(CustomerPath).Split('\n');

            //foreach (string item in userValues)
            //{
            //  if (!string.IsNullOrEmpty(item))
            //{
            //  string[] row = item.Split(",");
            Guid userId = Guid.Parse("5bfdfa9f-ffa2-4c31-40de-08db05cf468e");
            User user = new User()
            {
                Id = userId,
                FirstName = "tester",
                LastName = "here",
                EmailAddress = "tester@gmail.com",
                CreatedBy = Guid.NewGuid(),
                CreatedAt = new DateTime().ToString(),
            };


            Address address = new Address()
            {
                UserId = userId,
                Id = Guid.NewGuid(),
                Line1 = "anna nagar",
                Line2 = "velachery",
                City = "chennai",
                StateName = "tamil nadu",
                Type = "personal",
                Country = "tamil nadu",
                Zipcode = "626101",
                CreatedBy = userId,
                CreatedAt = new DateTime().ToString(),
                PhoneNumber = "1234567890",
            };

            CardDetail cardDetails = new CardDetail()
            {
                CardNumber = "1234 5678 90",
                CVVNo = "233",
                ExpiryDate = "24/23",
                HolderName = "tester",
                Type = "personal",
                Id = Guid.NewGuid(),
                UserId = userId,
            };

            UserCredential credential = new UserCredential()
            {
                Id = Guid.NewGuid(),
                UserName = "tester",
                Password = "tester2001",
                Role = "admin",
                UserId = userId,
            };
            modelBuilder.Entity<Address>().HasData(address);
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<CardDetail>().HasData(cardDetails);
            modelBuilder.Entity<UserCredential>().HasData(credential);
            //}
            //}

            base.OnModelCreating(modelBuilder);
        }
    }
}
