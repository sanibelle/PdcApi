using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removing_trailing_spaces_to_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""Competencies"" 
                SET ""ProgramOfStudyCode"" = RTRIM(""ProgramOfStudyCode"")
                WHERE ""ProgramOfStudyCode"" LIKE '% ';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
