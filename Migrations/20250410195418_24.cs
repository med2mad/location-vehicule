using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Charges");

            migrationBuilder.AddColumn<int>(
                name: "Annees",
                table: "Charges",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jours",
                table: "Charges",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mois",
                table: "Charges",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annees",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "jours",
                table: "Charges");

            migrationBuilder.DropColumn(
                name: "mois",
                table: "Charges");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Charges",
                type: "datetime2",
                nullable: true);
        }
    }
}
