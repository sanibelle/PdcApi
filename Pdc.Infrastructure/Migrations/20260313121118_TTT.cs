using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TTT : Migration
    // Migration that did not changed much after updating the ef core entities with the virtual accessor.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_ProgramOfStudies_ProgramOfStudyCode",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_Units_UnitsId",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                table: "ComplementaryInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentElements_CourseFrameworkPerformanceCriterias_CourseF~",
                table: "ContentElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_RealisationContexts_Competencies_CompetencyCode",
                table: "RealisationContexts");

            migrationBuilder.AlterColumn<string>(
                name: "CompetencyCode",
                table: "RealisationContexts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "GeneralUnitsId",
                table: "ProgramOfStudies",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComplementaryUnitsId",
                table: "ProgramOfStudies",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitsId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentVersionId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "CourseFrameworkCourseCode",
                table: "CourseFrameworkPerformanceCriterias",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CourseFrameworkCourseCode",
                table: "CourseFrameworkCompetencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseFrameworkPerformanceCriteriaId",
                table: "ContentElements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "WrittenOnVersionId",
                table: "ComplementaryInformations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramOfStudyCode",
                table: "Competencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentVersionId",
                table: "Competencies",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                table: "Competencies",
                column: "CurrentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_ProgramOfStudies_ProgramOfStudyCode",
                table: "Competencies",
                column: "ProgramOfStudyCode",
                principalTable: "ProgramOfStudies",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_Units_UnitsId",
                table: "Competencies",
                column: "UnitsId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                table: "ComplementaryInformations",
                column: "WrittenOnVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentElements_CourseFrameworkPerformanceCriterias_CourseF~",
                table: "ContentElements",
                column: "CourseFrameworkPerformanceCriteriaId",
                principalTable: "CourseFrameworkPerformanceCriterias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkCourseCode",
                principalTable: "CourseFrameworks",
                principalColumn: "CourseCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkCourseCode",
                principalTable: "CourseFrameworks",
                principalColumn: "CourseCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                table: "CourseFrameworks",
                column: "CurrentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RealisationContexts_Competencies_CompetencyCode",
                table: "RealisationContexts",
                column: "CompetencyCode",
                principalTable: "Competencies",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_ProgramOfStudies_ProgramOfStudyCode",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_Units_UnitsId",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                table: "ComplementaryInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentElements_CourseFrameworkPerformanceCriterias_CourseF~",
                table: "ContentElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_RealisationContexts_Competencies_CompetencyCode",
                table: "RealisationContexts");

            migrationBuilder.AlterColumn<string>(
                name: "CompetencyCode",
                table: "RealisationContexts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GeneralUnitsId",
                table: "ProgramOfStudies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ComplementaryUnitsId",
                table: "ProgramOfStudies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitsId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentVersionId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CourseFrameworkCourseCode",
                table: "CourseFrameworkPerformanceCriterias",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CourseFrameworkCourseCode",
                table: "CourseFrameworkCompetencies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseFrameworkPerformanceCriteriaId",
                table: "ContentElements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WrittenOnVersionId",
                table: "ComplementaryInformations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProgramOfStudyCode",
                table: "Competencies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentVersionId",
                table: "Competencies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                table: "Competencies",
                column: "CurrentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_ProgramOfStudies_ProgramOfStudyCode",
                table: "Competencies",
                column: "ProgramOfStudyCode",
                principalTable: "ProgramOfStudies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_Units_UnitsId",
                table: "Competencies",
                column: "UnitsId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                table: "ComplementaryInformations",
                column: "WrittenOnVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentElements_CourseFrameworkPerformanceCriterias_CourseF~",
                table: "ContentElements",
                column: "CourseFrameworkPerformanceCriteriaId",
                principalTable: "CourseFrameworkPerformanceCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkCourseCode",
                principalTable: "CourseFrameworks",
                principalColumn: "CourseCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkCourseCode",
                principalTable: "CourseFrameworks",
                principalColumn: "CourseCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                table: "CourseFrameworks",
                column: "CurrentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealisationContexts_Competencies_CompetencyCode",
                table: "RealisationContexts",
                column: "CompetencyCode",
                principalTable: "Competencies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
