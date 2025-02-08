using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Doctors.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_DoctorsShiftsAppointments_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorsShiftsAppointments",
                schema: "doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsShiftsAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsShiftsAppointments_DoctorsShifts_DoctorShiftId",
                        column: x => x.DoctorShiftId,
                        principalSchema: "doctors",
                        principalTable: "DoctorsShifts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsShiftsAppointments_DoctorShiftId",
                schema: "doctors",
                table: "DoctorsShiftsAppointments",
                column: "DoctorShiftId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsShiftsAppointments_DoctorShiftId_PatientId",
                schema: "doctors",
                table: "DoctorsShiftsAppointments",
                columns: new[] { "DoctorShiftId", "PatientId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsShiftsAppointments",
                schema: "doctors");
        }
    }
}
