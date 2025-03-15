using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Data.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_VehiculeId",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VehiculeId",
                table: "Locations",
                column: "VehiculeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_VehiculeId",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VehiculeId",
                table: "Locations",
                column: "VehiculeId",
                unique: true,
                filter: "[VehiculeId] IS NOT NULL");
        }
    }
}
