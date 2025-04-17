using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPtest.Migrations
{
    /// <inheritdoc />
    public partial class _29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depenses_Vehicules_VehiculeId",
                table: "Depenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_Locations_LocationId",
                table: "Paiements");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicules_Models_ModelId",
                table: "Vehicules");

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
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_Locations_LocationId",
                table: "Paiements",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicules_Models_ModelId",
                table: "Vehicules",
                column: "ModelId",
                principalTable: "Models",
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
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_Locations_LocationId",
                table: "Paiements");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicules_Models_ModelId",
                table: "Vehicules");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Vehicules_VehiculeId",
                table: "Locations",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_Locations_LocationId",
                table: "Paiements",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicules_Models_ModelId",
                table: "Vehicules",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vidanges_Vehicules_VehiculeId",
                table: "Vidanges",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitesTechniques_Vehicules_VehiculeId",
                table: "VisitesTechniques",
                column: "VehiculeId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
