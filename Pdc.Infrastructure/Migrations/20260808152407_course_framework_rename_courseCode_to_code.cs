using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class course_framework_rename_courseCode_to_code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "CourseFrameworks",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "PrerequisitesCourseCode",
                table: "CourseFrameworkPrerequisites",
                newName: "PrerequisitesCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkEntityCourseCode",
                table: "CourseFrameworkPrerequisites",
                newName: "CourseFrameworkEntityCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesCourseCode",
                table: "CourseFrameworkPrerequisites",
                newName: "IX_CourseFrameworkPrerequisites_PrerequisitesCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkCourseCode",
                table: "CourseFrameworkPerformanceCriterias",
                newName: "CourseFrameworkCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCourseCo~",
                table: "CourseFrameworkPerformanceCriterias",
                newName: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkCourseCode",
                table: "CourseFrameworkCompetencies",
                newName: "CourseFrameworkCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkCourseCode",
                table: "CourseFrameworkCompetencies",
                newName: "IX_CourseFrameworkCompetencies_CourseFrameworkCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkEntityCourseCode",
                table: "ChangeableEntityCourseFrameworkEntity",
                newName: "CourseFrameworkEntityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "CourseFrameworks",
                newName: "CourseCode");

            migrationBuilder.RenameColumn(
                name: "PrerequisitesCode",
                table: "CourseFrameworkPrerequisites",
                newName: "PrerequisitesCourseCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkEntityCode",
                table: "CourseFrameworkPrerequisites",
                newName: "CourseFrameworkEntityCourseCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesCode",
                table: "CourseFrameworkPrerequisites",
                newName: "IX_CourseFrameworkPrerequisites_PrerequisitesCourseCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkCode",
                table: "CourseFrameworkPerformanceCriterias",
                newName: "CourseFrameworkCourseCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCode",
                table: "CourseFrameworkPerformanceCriterias",
                newName: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCourseCo~");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkCode",
                table: "CourseFrameworkCompetencies",
                newName: "CourseFrameworkCourseCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkCode",
                table: "CourseFrameworkCompetencies",
                newName: "IX_CourseFrameworkCompetencies_CourseFrameworkCourseCode");

            migrationBuilder.RenameColumn(
                name: "CourseFrameworkEntityCode",
                table: "ChangeableEntityCourseFrameworkEntity",
                newName: "CourseFrameworkEntityCourseCode");
        }
    }
}
