using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mois",
                table: "Notifications",
                newName: "Mois");

            migrationBuilder.RenameColumn(
                name: "jours",
                table: "Notifications",
                newName: "Jours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mois",
                table: "Notifications",
                newName: "mois");

            migrationBuilder.RenameColumn(
                name: "Jours",
                table: "Notifications",
                newName: "jours");
        }
    }
}
