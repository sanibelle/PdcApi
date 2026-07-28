using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class course_framework_program_of_study_navigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CourseFrameworks",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "ProgramOfStudyId",
                table: "CourseFrameworks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_ProgramOfStudyId",
                table: "CourseFrameworks",
                column: "ProgramOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ProgramOfStudies_ProgramOfStudyId",
                table: "CourseFrameworks",
                column: "ProgramOfStudyId",
                principalTable: "ProgramOfStudies",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ProgramOfStudies_ProgramOfStudyId",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_ProgramOfStudyId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "ProgramOfStudyId",
                table: "CourseFrameworks");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CourseFrameworks",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400);
        }
    }
}
