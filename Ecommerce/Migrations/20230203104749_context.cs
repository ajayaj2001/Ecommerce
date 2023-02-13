using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Migrations
{
    public partial class context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("0a941351-fa32-4dc0-a38a-72664bf50dcf"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("85247c49-d4b3-43e6-bc5b-d031b797dfb7"));

            migrationBuilder.DeleteData(
                table: "credentials",
                keyColumn: "Id",
                keyValue: new Guid("4825d955-f047-441a-914f-8a180ee9e25a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("83c44a96-5f05-4df2-aca4-20f1d21497ab"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EmailAddress", "FirstName", "IsActive", "LastName", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "01-01-0001 12:00:00 AM", new Guid("b8473c2b-f7c1-443e-88f1-acdca9abaf5e"), "tester@gmail.com", "tester", true, "here", null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "IsActive", "Line1", "Line2", "PhoneNumber", "StateName", "Type", "UpdatedAt", "UpdatedBy", "UserId", "Zipcode" },
                values: new object[] { new Guid("d5dbdeba-4374-49d7-b419-4264fac23e4c"), "chennai", "tamil nadu", "01-01-0001 12:00:00 AM", new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), true, "anna nagar", "velachery", "1234567890", "tamil nadu", "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "626101" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CVVNo", "CardNumber", "CreatedAt", "CreatedBy", "ExpiryDate", "HolderName", "IsActive", "Type", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("916d820c-da4c-4771-8e89-8fc029eeeb30"), "233", "1234 5678 90", null, new Guid("00000000-0000-0000-0000-000000000000"), "24/23", "tester", true, "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });

            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Password", "Role", "UpdatedAt", "UpdatedBy", "UserId", "UserName" },
                values: new object[] { new Guid("0c4e13fb-abe0-45f3-a1c6-b55c2df8c15a"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, "tester2001", "customer", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "tester" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("d5dbdeba-4374-49d7-b419-4264fac23e4c"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("916d820c-da4c-4771-8e89-8fc029eeeb30"));

            migrationBuilder.DeleteData(
                table: "credentials",
                keyColumn: "Id",
                keyValue: new Guid("0c4e13fb-abe0-45f3-a1c6-b55c2df8c15a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EmailAddress", "FirstName", "IsActive", "LastName", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("83c44a96-5f05-4df2-aca4-20f1d21497ab"), "01-01-0001 12:00:00 AM", new Guid("ce88d5b0-be6c-44e0-a71b-d0964226c16c"), "tester@gmail.com", "tester", true, "here", null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "IsActive", "Line1", "Line2", "PhoneNumber", "StateName", "Type", "UpdatedAt", "UpdatedBy", "UserId", "Zipcode" },
                values: new object[] { new Guid("0a941351-fa32-4dc0-a38a-72664bf50dcf"), "chennai", "tamil nadu", "01-01-0001 12:00:00 AM", new Guid("83c44a96-5f05-4df2-aca4-20f1d21497ab"), true, "anna nagar", "velachery", "1234567890", "tamil nadu", "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("83c44a96-5f05-4df2-aca4-20f1d21497ab"), "626101" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CVVNo", "CardNumber", "CreatedAt", "CreatedBy", "ExpiryDate", "HolderName", "IsActive", "Type", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("85247c49-d4b3-43e6-bc5b-d031b797dfb7"), "233", "1234 5678 90", null, new Guid("00000000-0000-0000-0000-000000000000"), "24/23", "tester", true, "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("83c44a96-5f05-4df2-aca4-20f1d21497ab") });

            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Password", "Role", "UpdatedAt", "UpdatedBy", "UserId", "UserName" },
                values: new object[] { new Guid("4825d955-f047-441a-914f-8a180ee9e25a"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, "tester2001", "customer", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("83c44a96-5f05-4df2-aca4-20f1d21497ab"), "tester" });
        }
    }
}
