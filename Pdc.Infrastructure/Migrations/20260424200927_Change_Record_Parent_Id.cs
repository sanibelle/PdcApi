using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_Record_Parent_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentChangeRecordId",
                table: "ChangeRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE ""ChangeRecords""
                SET ""ParentChangeRecordId"" = ""ChangeRecordId""
                WHERE ""ChangeRecordId"" IS NOT NULL
            ");

            migrationBuilder.DropColumn(
                name: "ChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChangeableId",
                table: "ChangeDetails",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_ParentChangeRecordId",
                table: "ChangeRecords",
                column: "ParentChangeRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails",
                column: "ChangeableId",
                principalTable: "Changeables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ParentChangeRecordId",
                table: "ChangeRecords",
                column: "ParentChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ParentChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRecords_ParentChangeRecordId",
                table: "ChangeRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeRecordId",
                table: "ChangeRecords",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE ""ChangeRecords""
                SET ""ChangeRecordId"" = ""ParentChangeRecordId""
                WHERE ""ParentChangeRecordId"" IS NOT NULL
            ");

            migrationBuilder.DropColumn(
                name: "ParentChangeRecordId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords",
                column: "ChangeRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeDetails_Changeables_ChangeableId",
                table: "ChangeDetails",
                column: "ChangeableId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_ChangeRecordId",
                table: "ChangeRecords",
                column: "ChangeRecordId",
                principalTable: "ChangeRecords",
                principalColumn: "Id");
        }
    }
}
