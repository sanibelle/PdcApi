using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Version_to_ChangeRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_NextVersionId",
                table: "ChangeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ParentVersionId",
                table: "ChangeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                table: "ComplementaryInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                table: "CourseFrameworks");

            migrationBuilder.RenameColumn(
                name: "CurrentVersionId",
                table: "CourseFrameworks",
                newName: "ChangeRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworks_CurrentVersionId",
                table: "CourseFrameworks",
                newName: "IX_CourseFrameworks_ChangeRecordId");

            migrationBuilder.RenameColumn(
                name: "WrittenOnVersionId",
                table: "ComplementaryInformations",
                newName: "ChangeRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplementaryInformations_WrittenOnVersionId",
                table: "ComplementaryInformations",
                newName: "IX_ComplementaryInformations_ChangeRecordId");

            migrationBuilder.RenameColumn(
                name: "CurrentVersionId",
                table: "Competencies",
                newName: "ChangeRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Competencies_CurrentVersionId",
                table: "Competencies",
                newName: "IX_Competencies_ChangeRecordId");

            migrationBuilder.RenameColumn(
                name: "VersionNumber",
                table: "ChangeRecords",
                newName: "ChangeRecordNumber");

            migrationBuilder.RenameColumn(
                name: "ParentVersionId",
                table: "ChangeRecords",
                newName: "NextChangeRecordId");

            migrationBuilder.RenameColumn(
                name: "NextVersionId",
                table: "ChangeRecords",
                newName: "ChangeRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeRecords_ParentVersionId",
                table: "ChangeRecords",
                newName: "IX_ChangeRecords_NextChangeRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeRecords_NextVersionId",
                table: "ChangeRecords",
                newName: "IX_ChangeRecords_ChangeRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_NextChangeRecordId",
                table: "ChangeRecords",
                column: "NextChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_ChangeRecords_ChangeRecordId",
                table: "Competencies",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_ChangeRecordId",
                table: "ComplementaryInformations",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_ChangeRecordId",
                table: "CourseFrameworks",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_NextChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Competencies_ChangeRecords_ChangeRecordId",
                table: "Competencies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_ChangeRecordId",
                table: "ComplementaryInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_ChangeRecordId",
                table: "CourseFrameworks");

            migrationBuilder.RenameColumn(
                name: "ChangeRecordId",
                table: "CourseFrameworks",
                newName: "CurrentVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFrameworks_ChangeRecordId",
                table: "CourseFrameworks",
                newName: "IX_CourseFrameworks_CurrentVersionId");

            migrationBuilder.RenameColumn(
                name: "ChangeRecordId",
                table: "ComplementaryInformations",
                newName: "WrittenOnVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplementaryInformations_ChangeRecordId",
                table: "ComplementaryInformations",
                newName: "IX_ComplementaryInformations_WrittenOnVersionId");

            migrationBuilder.RenameColumn(
                name: "ChangeRecordId",
                table: "Competencies",
                newName: "CurrentVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_Competencies_ChangeRecordId",
                table: "Competencies",
                newName: "IX_Competencies_CurrentVersionId");

            migrationBuilder.RenameColumn(
                name: "NextChangeRecordId",
                table: "ChangeRecords",
                newName: "ParentVersionId");

            migrationBuilder.RenameColumn(
                name: "ChangeRecordNumber",
                table: "ChangeRecords",
                newName: "VersionNumber");

            migrationBuilder.RenameColumn(
                name: "ChangeRecordId",
                table: "ChangeRecords",
                newName: "NextVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeRecords_NextChangeRecordId",
                table: "ChangeRecords",
                newName: "IX_ChangeRecords_ParentVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords",
                newName: "IX_ChangeRecords_NextVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_NextVersionId",
                table: "ChangeRecords",
                column: "NextVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ParentVersionId",
                table: "ChangeRecords",
                column: "ParentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                table: "Competencies",
                column: "CurrentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                table: "ComplementaryInformations",
                column: "WrittenOnVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                table: "CourseFrameworks",
                column: "CurrentVersionId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");
        }
    }
}
