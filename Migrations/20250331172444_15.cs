using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id");
        }
    }
}
