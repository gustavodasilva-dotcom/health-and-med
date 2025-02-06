using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Doctors.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_DoctorsShifts_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorsShifts",
                schema: "doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_DoctorsShifts_DoctorsRegistrations_DoctorRegistrationId",
                        column: x => x.DoctorRegistrationId,
                        principalSchema: "doctors",
                        principalTable: "DoctorsRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsShifts_DoctorRegistrationId",
                schema: "doctors",
                table: "DoctorsShifts",
                column: "DoctorRegistrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsShifts",
                schema: "doctors");
        }
    }
}
