using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Data.Migrations
{
    /// <inheritdoc />
    public partial class m9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carburant",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "Climatise",
                table: "Models");

            migrationBuilder.AddColumn<string>(
                name: "Carburant",
                table: "Vehicules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Climatisation",
                table: "Vehicules",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carburant",
                table: "Vehicules");

            migrationBuilder.DropColumn(
                name: "Climatisation",
                table: "Vehicules");

            migrationBuilder.AddColumn<string>(
                name: "Carburant",
                table: "Models",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Climatise",
                table: "Models",
                type: "bit",
                nullable: true);
        }
    }
}
