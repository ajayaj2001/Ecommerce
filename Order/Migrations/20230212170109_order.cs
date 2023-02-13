using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.Migrations
{
    public partial class order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "OrderId", "ProductId", "Quantity", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf4622"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), 2, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });

            migrationBuilder.InsertData(
                table: "WishLists",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Name", "ProductId", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf4622"), null, new Guid("00000000-0000-0000-0000-000000000000"), true, "Personal", new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e"), null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bfdfa9f-ffa2-4c31-40de-08db05cf468e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "WishLists");
        }
    }
}
