using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "ChangeRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_CreatedById",
                table: "ChangeRecords",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_Users_CreatedById",
                table: "ChangeRecords",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_Users_CreatedById",
                table: "ChangeRecords");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRecords_CreatedById",
                table: "ChangeRecords");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ChangeRecords");
        }
    }
}
