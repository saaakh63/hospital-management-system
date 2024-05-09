using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospital.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Patient_ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doctor_Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Doctor_PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doctor_Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Department_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: true),
                    Doctor_ID = table.Column<int>(type: "int", nullable: true),
                    Patient_Id = table.Column<int>(type: "int", nullable: true),
                    Appointment_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Accounts_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Accounts",
                        principalColumn: "Patient_ID");
                    table.ForeignKey(
                        name: "FK_Appointments_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Department_Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientReports",
                columns: table => new
                {
                    PatientReport_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Report_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report_MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    prescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patient_ID = table.Column<int>(type: "int", nullable: true),
                    Doctor_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReports", x => x.PatientReport_Id);
                    table.ForeignKey(
                        name: "FK_PatientReports_Accounts_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Accounts",
                        principalColumn: "Patient_ID");
                    table.ForeignKey(
                        name: "FK_PatientReports_Doctors_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Department_Id",
                table: "Appointments",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctor_ID",
                table: "Appointments",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patient_Id",
                table: "Appointments",
                column: "Patient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Department_Id",
                table: "Doctors",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_Doctor_ID",
                table: "PatientReports",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_Patient_ID",
                table: "PatientReports",
                column: "Patient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "PatientReports");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
