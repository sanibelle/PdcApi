using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_unique_constraint_to_course_framework_competencies_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencies_Competencies_CompetencyId",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkId",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropColumn(
                name: "IsAssedElement",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.RenameColumn(
                name: "CompetencyId",
                table: "CourseFrameworkCompetencies",
                newName: "CompetencyCode");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkCompetencies_CompetencyId",
                table: "CourseFrameworkCompetencies",
                newName: "IX_CourseFrameworkCompetencies_CompetencyCode");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "CourseFrameworks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CourseFrameworks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "CourseFrameworkCompetencies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_CreatedById",
                table: "CourseFrameworks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkId_CompetencyCode",
                table: "CourseFrameworkCompetencies",
                columns: new[] { "CourseFrameworkId", "CompetencyCode" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencies_Competencies_CompetencyCode",
                table: "CourseFrameworkCompetencies",
                column: "CompetencyCode",
                principalTable: "Competencies",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_AspNetUsers_CreatedById",
                table: "CourseFrameworks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkCompetencies_Competencies_CompetencyCode",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_AspNetUsers_CreatedById",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworks_CreatedById",
                table: "CourseFrameworks");

            migrationBuilder.DropIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkId_CompetencyCode",
                table: "CourseFrameworkCompetencies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CourseFrameworks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CourseFrameworks");

            migrationBuilder.RenameColumn(
                name: "CompetencyCode",
                table: "CourseFrameworkCompetencies",
                newName: "CompetencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworkCompetencies_CompetencyCode",
                table: "CourseFrameworkCompetencies",
                newName: "IX_CourseFrameworkCompetencies_CompetencyId");

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "CourseFrameworkCompetencies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseFrameworkId",
                table: "CourseFrameworkCompetencies",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "IsAssedElement",
                table: "CourseFrameworkCompetencies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkId",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworkCompetencies_Competencies_CompetencyId",
                table: "CourseFrameworkCompetencies",
                column: "CompetencyId",
                principalTable: "Competencies",
                principalColumn: "Code");
        }
    }
}
