using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Depenses");

            migrationBuilder.AddColumn<int>(
                name: "ChargeId",
                table: "Depenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kilometrage = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depenses_ChargeId",
                table: "Depenses",
                column: "ChargeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Depenses_Charges_ChargeId",
                table: "Depenses",
                column: "ChargeId",
                principalTable: "Charges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depenses_Charges_ChargeId",
                table: "Depenses");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Depenses_ChargeId",
                table: "Depenses");

            migrationBuilder.DropColumn(
                name: "ChargeId",
                table: "Depenses");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Depenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
