using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMaintenanceVisitAndFridgeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianTechId",
                table: "FaultReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceVisits_Customers_CustomerId",
                table: "MaintenanceVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceVisits_Technicians_TechnicianId",
                table: "MaintenanceVisits");

            migrationBuilder.DropTable(
                name: "ServiceRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceVisits_CustomerId",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "FaultDescription",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "FollowUpDate",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "ResolvedDate",
                table: "FaultReports");

            migrationBuilder.DropColumn(
                name: "TechId",
                table: "FaultReports");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Technicians",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "MaintenanceVisits",
                newName: "TechnicianTechId");

            migrationBuilder.RenameColumn(
                name: "IsFollowUpRequired",
                table: "MaintenanceVisits",
                newName: "IsCompleted");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "MaintenanceVisits",
                newName: "TechId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceVisits_TechnicianId",
                table: "MaintenanceVisits",
                newName: "IX_MaintenanceVisits_TechnicianTechId");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Fridges",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TechnicianTechId",
                table: "FaultReports",
                newName: "TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_FaultReports_TechnicianTechId",
                table: "FaultReports",
                newName: "IX_FaultReports_TechnicianId");

            migrationBuilder.AlterColumn<string>(
                name: "TechName",
                table: "Technicians",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "MaintenanceVisits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "ActionsTaken",
                table: "MaintenanceVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuesFound",
                table: "MaintenanceVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitDate",
                table: "MaintenanceVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Fridges",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Fridges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Fridges",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TechId",
                table: "Fridges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicianTechId",
                table: "Fridges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FaultReports",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FridgeId = table.Column<int>(type: "int", nullable: false),
                    TechId = table.Column<int>(type: "int", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TechnicianTechId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalTable: "Fridges",
                        principalColumn: "FridgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Technicians_TechnicianTechId",
                        column: x => x.TechnicianTechId,
                        principalTable: "Technicians",
                        principalColumn: "TechId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_CustomerId",
                table: "Fridges",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_TechnicianTechId",
                table: "Fridges",
                column: "TechnicianTechId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_FridgeId",
                table: "MaintenanceRecords",
                column: "FridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_TechnicianTechId",
                table: "MaintenanceRecords",
                column: "TechnicianTechId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianId",
                table: "FaultReports",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_Customers_CustomerId",
                table: "Fridges",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_Technicians_TechnicianTechId",
                table: "Fridges",
                column: "TechnicianTechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceVisits_Technicians_TechnicianTechId",
                table: "MaintenanceVisits",
                column: "TechnicianTechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianId",
                table: "FaultReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_Customers_CustomerId",
                table: "Fridges");

            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_Technicians_TechnicianTechId",
                table: "Fridges");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceVisits_Technicians_TechnicianTechId",
                table: "MaintenanceVisits");

            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_CustomerId",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_TechnicianTechId",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "ActionsTaken",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "IssuesFound",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                table: "MaintenanceVisits");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "TechId",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "TechnicianTechId",
                table: "Fridges");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Technicians",
                newName: "Specialization");

            migrationBuilder.RenameColumn(
                name: "TechnicianTechId",
                table: "MaintenanceVisits",
                newName: "TechnicianId");

            migrationBuilder.RenameColumn(
                name: "TechId",
                table: "MaintenanceVisits",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "MaintenanceVisits",
                newName: "IsFollowUpRequired");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceVisits_TechnicianTechId",
                table: "MaintenanceVisits",
                newName: "IX_MaintenanceVisits_TechnicianId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Fridges",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "FaultReports",
                newName: "TechnicianTechId");

            migrationBuilder.RenameIndex(
                name: "IX_FaultReports_TechnicianId",
                table: "FaultReports",
                newName: "IX_FaultReports_TechnicianTechId");

            migrationBuilder.AlterColumn<string>(
                name: "TechName",
                table: "Technicians",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Technicians",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Technicians",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "MaintenanceVisits",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "MaintenanceVisits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "MaintenanceVisits",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaultDescription",
                table: "MaintenanceVisits",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FollowUpDate",
                table: "MaintenanceVisits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MaintenanceVisits",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Fridges",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FaultReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResolvedDate",
                table: "FaultReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechId",
                table: "FaultReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ServiceRecords",
                columns: table => new
                {
                    ServiceRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FridgeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecords", x => x.ServiceRecordId);
                    table.ForeignKey(
                        name: "FK_ServiceRecords_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalTable: "Fridges",
                        principalColumn: "FridgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceVisits_CustomerId",
                table: "MaintenanceVisits",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_FridgeId",
                table: "ServiceRecords",
                column: "FridgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaultReports_Technicians_TechnicianTechId",
                table: "FaultReports",
                column: "TechnicianTechId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceVisits_Customers_CustomerId",
                table: "MaintenanceVisits",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceVisits_Technicians_TechnicianId",
                table: "MaintenanceVisits",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "TechId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
