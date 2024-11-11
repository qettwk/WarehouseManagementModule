using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AutomobileId",
                schema: "WarehouseManagment",
                table: "RecordWrites",
                newName: "AutomobileID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                schema: "WarehouseManagment",
                table: "RecordWrites",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "WarehouseManagment",
                table: "Automobiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                schema: "WarehouseManagment",
                table: "RecordWrites");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "WarehouseManagment",
                table: "Automobiles");

            migrationBuilder.RenameColumn(
                name: "AutomobileID",
                schema: "WarehouseManagment",
                table: "RecordWrites",
                newName: "AutomobileId");
        }
    }
}
