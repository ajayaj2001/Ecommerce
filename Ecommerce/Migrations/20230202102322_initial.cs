using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Line1 = table.Column<string>(nullable: true),
                    Line2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    StateName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    HolderName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<string>(nullable: true),
                    CVVNo = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_credentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_credentials_UserId",
                table: "credentials",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
