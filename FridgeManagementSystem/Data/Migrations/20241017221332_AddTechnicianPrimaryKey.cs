using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeManagementSystem.Data.Migrations
{
    public partial class AddTechnicianPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.UniqueConstraint("AK_Customers_Email", x => x.Email); // Unique constraint on Email
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    TechId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.TechId);
                    table.UniqueConstraint("AK_Technicians_Email", x => x.Email); // Unique constraint on Email
                });

            migrationBuilder.CreateTable(
                name: "Fridges",
                columns: table => new
                {
                    FridgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianTechId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridges", x => x.FridgeId);
                    table.ForeignKey(
                        name: "FK_Fridges_Technicians_TechnicianTechId",
                        column: x => x.TechnicianTechId,
                        principalTable: "Technicians",
                        principalColumn: "TechId",
                        onDelete: ReferentialAction.NoAction // Prevent cascading deletes
                    );
                });

            migrationBuilder.CreateTable(
                name: "FaultReports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FridgeId = table.Column<int>(type: "int", nullable: false),
                    TechId = table.Column<int>(type: "int", nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    TechnicianTechId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultReports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_FaultReports_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalTable: "Fridges",
                        principalColumn: "FridgeId",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_FaultReports_Technicians_TechnicianTechId",
                        column: x => x.TechnicianTechId,
                        principalTable: "Technicians",
                        principalColumn: "TechId",
                        onDelete: ReferentialAction.NoAction // Prevent cascading deletes
                    );
                });

            // Add indices for foreign keys
            migrationBuilder.CreateIndex(
                name: "IX_Fridges_TechnicianTechId",
                table: "Fridges",
                column: "TechnicianTechId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultReports_FridgeId",
                table: "FaultReports",
                column: "FridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultReports_TechnicianTechId",
                table: "FaultReports",
                column: "TechnicianTechId");

            // Additional index for TechId in FaultReports
            migrationBuilder.CreateIndex(
                name: "IX_FaultReports_TechId",
                table: "FaultReports",
                column: "TechId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaultReports");

            migrationBuilder.DropTable(
                name: "Fridges");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
