using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quartiers_Villes_VilleId",
                table: "Quartiers");

            migrationBuilder.DropTable(
                name: "Villes");

            migrationBuilder.DropIndex(
                name: "IX_Quartiers_VilleId",
                table: "Quartiers");

            migrationBuilder.DropColumn(
                name: "VilleId",
                table: "Quartiers");

            migrationBuilder.AddColumn<string>(
                name: "Ville",
                table: "Quartiers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ville",
                table: "Quartiers");

            migrationBuilder.AddColumn<int>(
                name: "VilleId",
                table: "Quartiers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Villes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quartiers_VilleId",
                table: "Quartiers",
                column: "VilleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quartiers_Villes_VilleId",
                table: "Quartiers",
                column: "VilleId",
                principalTable: "Villes",
                principalColumn: "Id");
        }
    }
}
