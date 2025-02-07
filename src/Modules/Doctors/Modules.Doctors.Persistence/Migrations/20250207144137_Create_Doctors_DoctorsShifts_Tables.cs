using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Doctors.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_Doctors_DoctorsShifts_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "doctors");

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Ssn = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(97)", maxLength: 97, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsShifts",
                schema: "doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsShifts_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "doctors",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_RegistrationNumber",
                schema: "doctors",
                table: "Doctors",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsShifts_DoctorId",
                schema: "doctors",
                table: "DoctorsShifts",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsShifts",
                schema: "doctors");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "doctors");
        }
    }
}
