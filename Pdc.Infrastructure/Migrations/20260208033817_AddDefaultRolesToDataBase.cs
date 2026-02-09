using Microsoft.EntityFrameworkCore.Migrations;
using Pdc.Infrastructure.Identity;
using System.Globalization;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRolesToDataBase : Migration
    {
        private string[] roles =
        {
                Roles.StudyProgram,
                Roles.Competency,
                Roles.Admin,
                Roles.User
            };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert default roles into the Roles table
            foreach (var role in roles)
            {
                migrationBuilder.InsertData(
                    table: "AspNetRoles",
                    columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                    values: new object[] { Guid.NewGuid(), role, ToUpper(role), Guid.NewGuid().ToString() }
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var role in roles)
            {
                migrationBuilder.DeleteData(
                    table: "AspNetRoles",
                    keyColumn: "Name",
                    keyValues: new object[] { role }
                );
            }
        }

        private string ToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            return textInfo.ToUpper(input);
        }
    }
}
