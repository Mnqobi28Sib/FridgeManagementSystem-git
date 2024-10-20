using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeManagementSystem.Data.Migrations
{
    public partial class UpdateFridgesTechniciansConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop old foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianId",
                table: "FaultReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceVisits_Fridges_FridgeId",
                table: "MaintenanceVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceVisits_Technicians_TechnicianTechId",
                table: "MaintenanceVisits");

            // Drop and rename columns
            migrationBuilder.DropIndex(
                name: "IX_MaintenanceVisits_TechnicianTechId",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "ActionsTaken",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "IssuesFound",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                table: "MaintenanceVisits");

            // Rename columns to better reflect usage
            migrationBuilder.RenameColumn(
                name: "TechnicianTechId",
                table: "MaintenanceVisits",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "FaultReports",
                newName: "TechnicianTechId");

            migrationBuilder.RenameIndex(
                name: "IX_FaultReports_TechnicianId",
                table: "FaultReports",
                newName: "IX_FaultReports_TechnicianTechId");

            // Alter columns, setting lengths and nullable fields
            migrationBuilder.AlterColumn<string>(
                name: "TechName",
                table: "Technicians",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "MaintenanceVisits",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            // Add new columns
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "MaintenanceVisits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TechId",
                table: "Fridges",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperational",
                table: "Fridges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReportedDate",
                table: "FaultReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FaultReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "TechId",
                table: "FaultReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            // Create indexes for new foreign keys
            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceVisits_TechId",
                table: "MaintenanceVisits",
                column: "TechId");

            // Add foreign keys back with updated constraints
            migrationBuilder.AddForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianTechId",
                table: "FaultReports",
                column: "TechnicianTechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceVisits_Fridges_FridgeId",
                table: "MaintenanceVisits",
                column: "FridgeId",
                principalTable: "Fridges",
                principalColumn: "FridgeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceVisits_Technicians_TechId",
                table: "MaintenanceVisits",
                column: "TechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Code to reverse the migration
            // Drop foreign keys, restore dropped columns, and reverse renames
        }
    }
}
