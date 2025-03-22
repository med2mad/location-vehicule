using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Data.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaceFin",
                table: "Locations",
                newName: "LieuRetour");

            migrationBuilder.RenameColumn(
                name: "PlaceDebut",
                table: "Locations",
                newName: "LieuDepart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LieuRetour",
                table: "Locations",
                newName: "PlaceFin");

            migrationBuilder.RenameColumn(
                name: "LieuDepart",
                table: "Locations",
                newName: "PlaceDebut");
        }
    }
}
