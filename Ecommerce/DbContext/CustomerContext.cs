using Microsoft.EntityFrameworkCore;
using System;
using Customer.Entities.Models;
using System.IO;

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
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string ecommercePath = Path.Combine(baseDir, @"..\..\..\DbContext\data\Ecommerce.csv");
            string[] userValues = File.ReadAllText(Path.GetFullPath(ecommercePath)).Split('\n');

            foreach (string item in userValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        User user = new User()
                        {
                            Id = Guid.Parse(row[0]),
                            FirstName = row[1],
                            LastName = row[2],
                            EmailAddress = row[3],
                            CreatedBy = Guid.Parse(row[0]),
                            CreatedAt = new DateTime().ToString(),
                        };


                        Address address = new Address()
                        {
                            UserId = Guid.Parse(row[0]),
                            Id = Guid.Parse(row[4]),
                            Line1 = row[5],
                            Line2 = row[6],
                            City = row[7],
                            StateName = row[8],
                            Type = row[9],
                            Country = row[10],
                            Zipcode = row[11],
                            CreatedBy = Guid.Parse(row[0]),
                            CreatedAt = new DateTime().ToString(),
                            PhoneNumber = row[12],
                        };

                        CardDetail cardDetails = new CardDetail()
                        {
                            CardNumber = row[13],
                            CVVNo = row[14],
                            ExpiryDate = row[15],
                            HolderName = row[16],
                            Type = row[17],
                            Id = Guid.Parse(row[18]),
                            UserId = Guid.Parse(row[0]),
                        };

                        UserCredential credential = new UserCredential()
                        {
                            Id = Guid.Parse(row[19]),
                            UserName = row[20],
                            Password = row[21],
                            Role = row[22],
                            UserId = Guid.Parse(row[0]),
                        };
                        modelBuilder.Entity<Address>().HasData(address);
                        modelBuilder.Entity<User>().HasData(user);
                        modelBuilder.Entity<CardDetail>().HasData(cardDetails);
                        modelBuilder.Entity<UserCredential>().HasData(credential);
                    }
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
