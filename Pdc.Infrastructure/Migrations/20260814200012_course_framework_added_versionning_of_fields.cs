using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class course_framework_added_versionning_of_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworks_Cour~",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencyElements_CourseFrameworks_CourseFr~",
                table: "CourseFrameworkCompetencyElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_CourseFramewo~",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_Prerequisites~",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ProgramOfStudies_ProgramOfStudyId",
                table: "CourseFrameworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_Units_UnitsId",
                table: "CourseFrameworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFrameworks",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_UnitsId",
                table: "CourseFrameworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFrameworkPrerequisites",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesCode",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCode",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkCode",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeableEntityCourseFrameworkEntity",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntity~",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "UnitsId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "Weighting_LaboratoryHours",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "Weighting_PersonnalWorkHours",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "Weighting_TheoryHours",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkEntityCode",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropColumn(
                name: "PrerequisitesCode",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkCode",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkCode",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkEntityCode",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.RenameColumn(
                name: "ProgramOfStudyId",
                table: "CourseFrameworks",
                newName: "ProgramOfStudyCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworks_ProgramOfStudyId",
                table: "CourseFrameworks",
                newName: "IX_CourseFrameworks_ProgramOfStudyCode");

            migrationBuilder.AlterColumn<string>(
                name: "OtherSpecifications",
                table: "CourseFrameworks",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(5000)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CodeId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LaboratoryHoursId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonnalWorkHoursId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TheoryHoursId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseFrameworkEntityId",
                table: "CourseFrameworkPrerequisites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrerequisitesId",
                table: "CourseFrameworkPrerequisites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseFrameworkId",
                table: "CourseFrameworkPerformanceCriterias",
                type: "uuid",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencyElements");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencyElements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseFrameworkEntityId",
                table: "ChangeableEntityCourseFrameworkEntity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFrameworks",
                table: "CourseFrameworks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFrameworkPrerequisites",
                table: "CourseFrameworkPrerequisites",
                columns: new[] { "CourseFrameworkEntityId", "PrerequisitesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeableEntityCourseFrameworkEntity",
                table: "ChangeableEntityCourseFrameworkEntity",
                columns: new[] { "AssedElementsId", "CourseFrameworkEntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_CodeId",
                table: "CourseFrameworks",
                column: "CodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_LaboratoryHoursId",
                table: "CourseFrameworks",
                column: "LaboratoryHoursId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_NameId",
                table: "CourseFrameworks",
                column: "NameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_PersonnalWorkHoursId",
                table: "CourseFrameworks",
                column: "PersonnalWorkHoursId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_SemesterId",
                table: "CourseFrameworks",
                column: "SemesterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_TheoryHoursId",
                table: "CourseFrameworks",
                column: "TheoryHoursId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesId",
                table: "CourseFrameworkPrerequisites",
                column: "PrerequisitesId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkId",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkId",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntity~",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "CourseFrameworkEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworks_Cour~",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "CourseFrameworkEntityId",
                principalTable: "CourseFrameworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkId",
                principalTable: "CourseFrameworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencyElements_CourseFrameworks_CourseFr~",
                table: "CourseFrameworkCompetencyElements",
                column: "CourseFrameworkId",
                principalTable: "CourseFrameworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkId",
                principalTable: "CourseFrameworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_CourseFramewo~",
                table: "CourseFrameworkPrerequisites",
                column: "CourseFrameworkEntityId",
                principalTable: "CourseFrameworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_Prerequisites~",
                table: "CourseFrameworkPrerequisites",
                column: "PrerequisitesId",
                principalTable: "CourseFrameworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ProgramOfStudies_ProgramOfStudyCode",
                table: "CourseFrameworks",
                column: "ProgramOfStudyCode",
                principalTable: "ProgramOfStudies",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworks_Cour~",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencyElements_CourseFrameworks_CourseFr~",
                table: "CourseFrameworkCompetencyElements");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_CourseFramewo~",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_Prerequisites~",
                table: "CourseFrameworkPrerequisites");

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

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ProgramOfStudies_ProgramOfStudyCode",
                table: "CourseFrameworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFrameworks",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_CodeId",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_LaboratoryHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_NameId",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_PersonnalWorkHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_SemesterId",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_TheoryHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseFrameworkPrerequisites",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesId",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkId",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkId",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeableEntityCourseFrameworkEntity",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntity~",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "LaboratoryHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "PersonnalWorkHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "TheoryHoursId",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkEntityId",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropColumn(
                name: "PrerequisitesId",
                table: "CourseFrameworkPrerequisites");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkId",
                table: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropColumn(
                name: "CourseFrameworkEntityId",
                table: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.RenameColumn(
                name: "ProgramOfStudyCode",
                table: "CourseFrameworks",
                newName: "ProgramOfStudyId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworks_ProgramOfStudyCode",
                table: "CourseFrameworks",
                newName: "IX_CourseFrameworks_ProgramOfStudyId");

            migrationBuilder.AlterColumn<string>(
                name: "OtherSpecifications",
                table: "CourseFrameworks",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5000)",
                oldMaxLength: 5000);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CourseFrameworks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Hours",
                table: "CourseFrameworks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CourseFrameworks",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Semester",
                table: "CourseFrameworks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitsId",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Weighting_LaboratoryHours",
                table: "CourseFrameworks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Weighting_PersonnalWorkHours",
                table: "CourseFrameworks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Weighting_TheoryHours",
                table: "CourseFrameworks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "CourseFrameworkEntityCode",
                table: "CourseFrameworkPrerequisites",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrerequisitesCode",
                table: "CourseFrameworkPrerequisites",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseFrameworkCode",
                table: "CourseFrameworkPerformanceCriterias",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencyElements");

            migrationBuilder.AddColumn<string>(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencyElements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseFrameworkCode",
                table: "CourseFrameworkCompetencies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseFrameworkEntityCode",
                table: "ChangeableEntityCourseFrameworkEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFrameworks",
                table: "CourseFrameworks",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseFrameworkPrerequisites",
                table: "CourseFrameworkPrerequisites",
                columns: new[] { "CourseFrameworkEntityCode", "PrerequisitesCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeableEntityCourseFrameworkEntity",
                table: "ChangeableEntityCourseFrameworkEntity",
                columns: new[] { "AssedElementsId", "CourseFrameworkEntityCode" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_UnitsId",
                table: "CourseFrameworks",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesCode",
                table: "CourseFrameworkPrerequisites",
                column: "PrerequisitesCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCode",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkCode",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkCode");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntity~",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "CourseFrameworkEntityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworks_Cour~",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "CourseFrameworkEntityCode",
                principalTable: "CourseFrameworks",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkCode",
                principalTable: "CourseFrameworks",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencyElements_CourseFrameworks_CourseFr~",
                table: "CourseFrameworkCompetencyElements",
                column: "CourseFrameworkId",
                principalTable: "CourseFrameworks",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkCode",
                principalTable: "CourseFrameworks",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_CourseFramewo~",
                table: "CourseFrameworkPrerequisites",
                column: "CourseFrameworkEntityCode",
                principalTable: "CourseFrameworks",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_Prerequisites~",
                table: "CourseFrameworkPrerequisites",
                column: "PrerequisitesCode",
                principalTable: "CourseFrameworks",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ProgramOfStudies_ProgramOfStudyId",
                table: "CourseFrameworks",
                column: "ProgramOfStudyId",
                principalTable: "ProgramOfStudies",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_Units_UnitsId",
                table: "CourseFrameworks",
                column: "UnitsId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
