using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depenses_Vehicules_VehiculeId",
                table: "Depenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vidanges_Vehicules_VehiculeId",
                table: "Vidanges");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitesTechniques_Vehicules_VehiculeId",
                table: "VisitesTechniques");

            migrationBuilder.AddForeignKey(
                name: "FK_Depenses_Vehicules_VehiculeId",
                table: "Depenses",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vidanges_Vehicules_VehiculeId",
                table: "Vidanges",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitesTechniques_Vehicules_VehiculeId",
                table: "VisitesTechniques",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depenses_Vehicules_VehiculeId",
                table: "Depenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Vidanges_Vehicules_VehiculeId",
                table: "Vidanges");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitesTechniques_Vehicules_VehiculeId",
                table: "VisitesTechniques");

            migrationBuilder.AddForeignKey(
                name: "FK_Depenses_Vehicules_VehiculeId",
                table: "Depenses",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vidanges_Vehicules_VehiculeId",
                table: "Vidanges",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitesTechniques_Vehicules_VehiculeId",
                table: "VisitesTechniques",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id");
        }
    }
}
