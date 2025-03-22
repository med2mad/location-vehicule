using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Data.Migrations
{
    /// <inheritdoc />
    public partial class m7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateFin",
                table: "Locations",
                newName: "DateRetour");

            migrationBuilder.RenameColumn(
                name: "DateDebut",
                table: "Locations",
                newName: "DateDepart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateRetour",
                table: "Locations",
                newName: "DateFin");

            migrationBuilder.RenameColumn(
                name: "DateDepart",
                table: "Locations",
                newName: "DateDebut");
        }
    }
}
