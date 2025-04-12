using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depenses_Charges_ChargeId",
                table: "Depenses");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.RenameColumn(
                name: "ChargeId",
                table: "Depenses",
                newName: "NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Depenses_ChargeId",
                table: "Depenses",
                newName: "IX_Depenses_NotificationId");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kilometrage = table.Column<int>(type: "int", nullable: true),
                    jours = table.Column<int>(type: "int", nullable: true),
                    mois = table.Column<int>(type: "int", nullable: true),
                    Annees = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Depenses_Notifications_NotificationId",
                table: "Depenses",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depenses_Notifications_NotificationId",
                table: "Depenses");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Depenses",
                newName: "ChargeId");

            migrationBuilder.RenameIndex(
                name: "IX_Depenses_NotificationId",
                table: "Depenses",
                newName: "IX_Depenses_ChargeId");

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Annees = table.Column<int>(type: "int", nullable: true),
                    Kilometrage = table.Column<int>(type: "int", nullable: true),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jours = table.Column<int>(type: "int", nullable: true),
                    mois = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Depenses_Charges_ChargeId",
                table: "Depenses",
                column: "ChargeId",
                principalTable: "Charges",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
