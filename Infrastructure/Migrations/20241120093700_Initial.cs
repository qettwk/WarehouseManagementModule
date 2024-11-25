using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WarehouseManagment");

            migrationBuilder.CreateTable(
                name: "Automobiles",
                schema: "WarehouseManagment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "WarehouseManagment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordWrites",
                schema: "WarehouseManagment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AutomobileID = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordWrites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutomobileCategory",
                schema: "WarehouseManagment",
                columns: table => new
                {
                    AutomobilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomobileCategory", x => new { x.AutomobilesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_AutomobileCategory_Automobiles_AutomobilesId",
                        column: x => x.AutomobilesId,
                        principalSchema: "WarehouseManagment",
                        principalTable: "Automobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutomobileCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalSchema: "WarehouseManagment",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutomobileCategory_CategoriesId",
                schema: "WarehouseManagment",
                table: "AutomobileCategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutomobileCategory",
                schema: "WarehouseManagment");

            migrationBuilder.DropTable(
                name: "RecordWrites",
                schema: "WarehouseManagment");

            migrationBuilder.DropTable(
                name: "Automobiles",
                schema: "WarehouseManagment");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "WarehouseManagment");
        }
    }
}
