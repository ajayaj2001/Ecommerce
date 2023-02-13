using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Migrations
{
    public partial class customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "IsActive", "Line1", "Line2", "PhoneNumber", "StateName", "Type", "UpdatedAt", "UpdatedBy", "UserId", "Zipcode" },
                values: new object[] { new Guid("cbdbfada-04e8-479f-b71c-dfd9b15bf146"), "chennai", "tamil nadu", "01-01-0001 12:00:00 AM", new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), true, "anna nagar", "velachery", "1234567890", "tamil nadu", "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "626101" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CVVNo", "CardNumber", "CreatedAt", "CreatedBy", "ExpiryDate", "HolderName", "IsActive", "Type", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("b265a47d-41fb-48dd-a5a9-de9fd6715695"), "233", "1234 5678 90", null, new Guid("00000000-0000-0000-0000-000000000000"), "24/23", "tester", true, "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                column: "CreatedBy",
                value: new Guid("4cbdbef2-9410-4dbe-93ef-940520e30c6d"));

            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Password", "Role", "UpdatedAt", "UpdatedBy", "UserId", "UserName" },
                values: new object[] { new Guid("f0957a73-e62a-440e-9914-d089914bbfaa"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, "tester2001", "admin", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "tester" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("cbdbfada-04e8-479f-b71c-dfd9b15bf146"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("b265a47d-41fb-48dd-a5a9-de9fd6715695"));

            migrationBuilder.DeleteData(
                table: "credentials",
                keyColumn: "Id",
                keyValue: new Guid("f0957a73-e62a-440e-9914-d089914bbfaa"));

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "IsActive", "Line1", "Line2", "PhoneNumber", "StateName", "Type", "UpdatedAt", "UpdatedBy", "UserId", "Zipcode" },
                values: new object[] { new Guid("d5dbdeba-4374-49d7-b419-4264fac23e4c"), "chennai", "tamil nadu", "01-01-0001 12:00:00 AM", new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), true, "anna nagar", "velachery", "1234567890", "tamil nadu", "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "626101" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CVVNo", "CardNumber", "CreatedAt", "CreatedBy", "ExpiryDate", "HolderName", "IsActive", "Type", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("916d820c-da4c-4771-8e89-8fc029eeeb30"), "233", "1234 5678 90", null, new Guid("00000000-0000-0000-0000-000000000000"), "24/23", "tester", true, "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                column: "CreatedBy",
                value: new Guid("b8473c2b-f7c1-443e-88f1-acdca9abaf5e"));

            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Password", "Role", "UpdatedAt", "UpdatedBy", "UserId", "UserName" },
                values: new object[] { new Guid("0c4e13fb-abe0-45f3-a1c6-b55c2df8c15a"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, "tester2001", "customer", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "tester" });
        }
    }
}
