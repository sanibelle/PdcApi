using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class course_framework_removed_required_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Changeables_CodeId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Changeables_LaboratoryHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Changeables_NameId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Changeables_PersonnalWorkHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Changeables_SemesterId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Changeables_TheoryHoursId",
                table: "CourseFrameworks");

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeRecordId",
                table: "ProgramOfStudies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseFrameworkChangeableEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseFrameworkId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkChangeableEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkChangeableEntity_Changeables_Id",
                        column: x => x.Id,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkChangeableEntity_CourseFrameworks_CourseFram~",
                        column: x => x.CourseFrameworkId,
                        principalTable: "CourseFrameworks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_ChangeRecordId",
                table: "ProgramOfStudies",
                column: "ChangeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkChangeableEntity_CourseFrameworkId",
                table: "CourseFrameworkChangeableEntity",
                column: "CourseFrameworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_CodeId",
                table: "CourseFrameworks",
                column: "CodeId",
                principalTable: "CourseFrameworkChangeableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_Laboratory~",
                table: "CourseFrameworks",
                column: "LaboratoryHoursId",
                principalTable: "CourseFrameworkChangeableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_NameId",
                table: "CourseFrameworks",
                column: "NameId",
                principalTable: "CourseFrameworkChangeableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_PersonnalW~",
                table: "CourseFrameworks",
                column: "PersonnalWorkHoursId",
                principalTable: "CourseFrameworkChangeableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_SemesterId",
                table: "CourseFrameworks",
                column: "SemesterId",
                principalTable: "CourseFrameworkChangeableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_TheoryHour~",
                table: "CourseFrameworks",
                column: "TheoryHoursId",
                principalTable: "CourseFrameworkChangeableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfStudies_ChangeRecords_ChangeRecordId",
                table: "ProgramOfStudies",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_CodeId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_Laboratory~",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_NameId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_PersonnalW~",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_SemesterId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_CourseFrameworkChangeableEntity_TheoryHour~",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfStudies_ChangeRecords_ChangeRecordId",
                table: "ProgramOfStudies");

            migrationBuilder.DropTable(
                name: "CourseFrameworkChangeableEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfStudies_ChangeRecordId",
                table: "ProgramOfStudies");

            migrationBuilder.DropColumn(
                name: "ChangeRecordId",
                table: "ProgramOfStudies");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Changeables_CodeId",
                table: "CourseFrameworks",
                column: "CodeId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Changeables_LaboratoryHoursId",
                table: "CourseFrameworks",
                column: "LaboratoryHoursId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Changeables_NameId",
                table: "CourseFrameworks",
                column: "NameId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Changeables_PersonnalWorkHoursId",
                table: "CourseFrameworks",
                column: "PersonnalWorkHoursId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Changeables_SemesterId",
                table: "CourseFrameworks",
                column: "SemesterId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Changeables_TheoryHoursId",
                table: "CourseFrameworks",
                column: "TheoryHoursId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
