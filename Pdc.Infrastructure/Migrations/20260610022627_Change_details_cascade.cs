using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_details_cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails");

            migrationBuilder.DropColumn(
                name: "ChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChangeableId",
                table: "ChangeDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeRecordEntityId",
                table: "ChangeDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetails_ChangeRecordEntityId",
                table: "ChangeDetails",
                column: "ChangeRecordEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeDetails_ChangeRecords_ChangeRecordEntityId",
                table: "ChangeDetails",
                column: "ChangeRecordEntityId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails",
                column: "ChangeableId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeDetails_ChangeRecords_ChangeRecordEntityId",
                table: "ChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails");

            migrationBuilder.DropIndex(
                name: "IX_ChangeDetails_ChangeRecordEntityId",
                table: "ChangeDetails");

            migrationBuilder.DropColumn(
                name: "ChangeRecordEntityId",
                table: "ChangeDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeRecordId",
                table: "ChangeRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChangeableId",
                table: "ChangeDetails",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails",
                column: "ChangeableId",
                principalTable: "Changeables",
                principalColumn: "Id");
        }
    }
}
