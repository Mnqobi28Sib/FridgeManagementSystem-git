using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTechnicianFridgeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_Technicians_TechId",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_TechId",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "TechniciTechId",
                table: "Fridges");

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_TechId",
                table: "Fridges",
                column: "TechId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_Technicians_TechId",
                table: "Fridges",
                column: "TechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_Technicians_TechId",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_TechId",
                table: "Fridges");

            migrationBuilder.AddColumn<int>(
                name: "TechnicianTechId",
                table: "Fridges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_TechnicianTechId",
                table: "Fridges",
                column: "TechnicianTechId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_Technicians_TechnicianTechId",
                table: "Fridges",
                column: "TechnicianTechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
