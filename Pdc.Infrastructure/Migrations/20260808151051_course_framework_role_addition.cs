using Microsoft.EntityFrameworkCore.Migrations;
using Pdc.Domain.Models.Security;
using System.Globalization;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class course_framework_role_addition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid(), Roles.CourseFramework, ToUpper(Roles.CourseFramework), Guid.NewGuid().ToString() }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValues: new object[] { Roles.CourseFramework }
            );
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
