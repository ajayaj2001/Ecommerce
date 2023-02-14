using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Customer.DbContexts;
using System.IO;
using Customer.Entities;
using System.Numerics;
using Customer.Entities.Models;
using System.Reflection.Emit;

namespace CustomerUnitTest.InMemoryContext
{
    public static class InMemorydbContext
    {
        /// <summary>
        /// This method is used to create the InMemeorydatabase
        /// </summary>
        public static CustomerContext customerContext()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new CustomerContext(options);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string ecommercePath = Path.Combine(baseDir, @"..\..\..\InMemoryContext\data\Ecommerce.csv");
            string[] userValues = File.ReadAllText(Path.GetFullPath(ecommercePath)).Split('\n');

            foreach (string item in userValues)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] row = item.Split(",");
                    {
                        context.Users.Add(new User()
                        {
                            Id = Guid.Parse(row[0]),//0
                            FirstName = row[1],//1
                            LastName = row[2],//2
                            EmailAddress = row[3],//3
                            CreatedBy = Guid.Parse(row[0]),//0
                            CreatedAt = new DateTime().ToString(),

                            Addresses = new List<Address>()
                        {
                        new Address(){
                            UserId = Guid.Parse(row[0]),//0
                            Id = Guid.Parse(row[4]),//4
                            Line1 = row[5],//5
                            Line2 = row[6],//6
                            City = row[7],//7
                            StateName = row[8],//8
                            Type = row[9],//9
                            Country = row[10],//10
                            Zipcode = row[11],//11
                            CreatedBy = Guid.Parse(row[0]),//0
                            CreatedAt = new DateTime().ToString(),
                            PhoneNumber = row[12],//12
                        }
                            },

                            CardDetails = new List<CardDetail>()
                            {
                               new CardDetail(){
                                CardNumber = row[13],//13
                                CVVNo = row[14],//14
                                ExpiryDate = row[15],//15
                                HolderName = row[16],//16
                                Type = row[17],//17
                                Id = Guid.Parse(row[18]),//18
                                UserId = Guid.Parse(row[0]),//0
                            }
                           },

                            Credentials = new UserCredential()
                            {
                                Id = Guid.Parse(row[19]),//19
                                UserName = row[20],//20
                                Password = row[21],//21
                                Role = row[22],//22
                                UserId = Guid.Parse(row[0]),//0
                            },
                        });
                    }
                }
            }


            return context;
        }
    }
}