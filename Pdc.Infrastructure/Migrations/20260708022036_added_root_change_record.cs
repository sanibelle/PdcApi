using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_root_change_record : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RootId",
                table: "ChangeRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Walk up the ParentChangeRecordId chain until ParentChangeRecordId IS NULL,
            // then assign that ancestor's Id as the RootId for every record in the chain.
            migrationBuilder.Sql(@"
                WITH RECURSIVE chain AS (
                    -- Anchor: records with no parent are their own root
                    SELECT ""Id"", ""ParentChangeRecordId"", ""Id"" AS ""RootId""
                    FROM ""ChangeRecords""
                    WHERE ""ParentChangeRecordId"" IS NULL

                    UNION ALL

                    -- Recursive: propagate the root down to children
                    SELECT cr.""Id"", cr.""ParentChangeRecordId"", chain.""RootId""
                    FROM ""ChangeRecords"" cr
                    INNER JOIN chain ON cr.""ParentChangeRecordId"" = chain.""Id""
                )
                UPDATE ""ChangeRecords""
                SET ""RootId"" = chain.""RootId""
                FROM chain
                WHERE ""ChangeRecords"".""Id"" = chain.""Id"";
            ");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_RootId",
                table: "ChangeRecords",
                column: "RootId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_RootId",
                table: "ChangeRecords",
                column: "RootId",
                principalTable: "ChangeRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeRecords_ChangeRecords_RootId",
                table: "ChangeRecords");

            migrationBuilder.DropIndex(
                name: "IX_ChangeRecords_RootId",
                table: "ChangeRecords");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "ChangeRecords");
        }
    }
}
