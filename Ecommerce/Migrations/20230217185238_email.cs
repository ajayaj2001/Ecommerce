using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Migrations
{
    public partial class email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "credentials");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "credentials",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "IsActive", "Line1", "Line2", "PhoneNumber", "StateName", "Type", "UpdatedAt", "UpdatedBy", "UserId", "Zipcode" },
                values: new object[] { new Guid("7cf56f52-1aab-4646-b090-d337aac18370"), "dindigul", "India", null, new Guid("00000000-0000-0000-0000-000000000000"), true, "psna college", "psna nagar", "1234567890", "tamilnadu", "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "626101" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CVVNo", "CardNumber", "CreatedAt", "CreatedBy", "ExpiryDate", "HolderName", "IsActive", "Type", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("dbe63d8c-7a58-4c10-a68f-4e7a903ef5a8"), "233", "1234 5678 90", null, new Guid("00000000-0000-0000-0000-000000000000"), "24/23", "tester", true, "personal", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"),
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EmailAddress", "IsActive", "Password", "Role", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("2162c878-9a7e-4316-a5d4-ecfc3651c8b0"), null, new Guid("00000000-0000-0000-0000-000000000000"), "tester@gmail.com", true, "tester2001", "admin", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("7cf56f52-1aab-4646-b090-d337aac18370"));

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("dbe63d8c-7a58-4c10-a68f-4e7a903ef5a8"));

            migrationBuilder.DeleteData(
                table: "credentials",
                keyColumn: "Id",
                keyValue: new Guid("2162c878-9a7e-4316-a5d4-ecfc3651c8b0"));

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "credentials");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "credentials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "CreatedAt", "CreatedBy", "EmailAddress" },
                values: new object[] { "01-01-0001 12:00:00 AM", new Guid("4cbdbef2-9410-4dbe-93ef-940520e30c6d"), "tester@gmail.com" });

            migrationBuilder.InsertData(
                table: "credentials",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Password", "Role", "UpdatedAt", "UpdatedBy", "UserId", "UserName" },
                values: new object[] { new Guid("f0957a73-e62a-440e-9914-d089914bbfaa"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, "tester2001", "admin", null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), "tester" });
        }
    }
}
