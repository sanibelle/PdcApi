using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removed_change_record_from_program_of_study_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfStudies_ChangeRecords_ChangeRecordId",
                table: "ProgramOfStudies");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfStudies_ChangeRecordId",
                table: "ProgramOfStudies");

            migrationBuilder.DropColumn(
                name: "ChangeRecordId",
                table: "ProgramOfStudies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChangeRecordId",
                table: "ProgramOfStudies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_ChangeRecordId",
                table: "ProgramOfStudies",
                column: "ChangeRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfStudies_ChangeRecords_ChangeRecordId",
                table: "ProgramOfStudies",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");
        }
    }
}
