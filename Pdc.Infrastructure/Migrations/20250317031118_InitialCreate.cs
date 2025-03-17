using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pdc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WholeUnits = table.Column<int>(type: "int", nullable: false),
                    Numerator = table.Column<int>(type: "int", nullable: true),
                    Denominator = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfStudies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OptionnalUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeneralUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplementaryUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sanction = table.Column<int>(type: "int", nullable: false),
                    MonthsDuration = table.Column<double>(type: "float", nullable: false),
                    SpecificDurationHours = table.Column<int>(type: "int", nullable: false),
                    TotalDurationHours = table.Column<int>(type: "int", nullable: false),
                    PublishedOn = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramOfStudies_Units_ComplementaryUnitsId",
                        column: x => x.ComplementaryUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramOfStudies_Units_GeneralUnitsId",
                        column: x => x.GeneralUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramOfStudies_Units_OptionnalUnitsId",
                        column: x => x.OptionnalUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramOfStudies_Units_SpecificUnitsId",
                        column: x => x.SpecificUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MinisterialCompetency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsOptionnal = table.Column<bool>(type: "bit", nullable: false),
                    StatementOfCompetency = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProgramOfStudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinisterialCompetency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinisterialCompetency_ProgramOfStudies_ProgramOfStudyId",
                        column: x => x.ProgramOfStudyId,
                        principalTable: "ProgramOfStudies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MinisterialCompetency_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompetencyElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    MinisterialCompetencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyElement_MinisterialCompetency_MinisterialCompetencyId",
                        column: x => x.MinisterialCompetencyId,
                        principalTable: "MinisterialCompetency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MinisterialRealisationContext",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinisterialCompetencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinisterialRealisationContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinisterialRealisationContext_MinisterialCompetency_MinisterialCompetencyId",
                        column: x => x.MinisterialCompetencyId,
                        principalTable: "MinisterialCompetency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CompetencyElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceCriteria_CompetencyElement_CompetencyElementId",
                        column: x => x.CompetencyElementId,
                        principalTable: "CompetencyElement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComplementaryInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementaryInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformations_PerformanceCriteria_ContentElementId",
                        column: x => x.ContentElementId,
                        principalTable: "PerformanceCriteria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyElement_MinisterialCompetencyId",
                table: "CompetencyElement",
                column: "MinisterialCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformations_ContentElementId",
                table: "ComplementaryInformations",
                column: "ContentElementId");

            migrationBuilder.CreateIndex(
                name: "IX_MinisterialCompetency_ProgramOfStudyId",
                table: "MinisterialCompetency",
                column: "ProgramOfStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_MinisterialCompetency_UnitsId",
                table: "MinisterialCompetency",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_MinisterialRealisationContext_MinisterialCompetencyId",
                table: "MinisterialRealisationContext",
                column: "MinisterialCompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceCriteria_CompetencyElementId",
                table: "PerformanceCriteria",
                column: "CompetencyElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_ComplementaryUnitsId",
                table: "ProgramOfStudies",
                column: "ComplementaryUnitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_GeneralUnitsId",
                table: "ProgramOfStudies",
                column: "GeneralUnitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_OptionnalUnitsId",
                table: "ProgramOfStudies",
                column: "OptionnalUnitsId",
                unique: true,
                filter: "[OptionnalUnitsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_SpecificUnitsId",
                table: "ProgramOfStudies",
                column: "SpecificUnitsId",
                unique: true,
                filter: "[SpecificUnitsId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplementaryInformations");

            migrationBuilder.DropTable(
                name: "MinisterialRealisationContext");

            migrationBuilder.DropTable(
                name: "PerformanceCriteria");

            migrationBuilder.DropTable(
                name: "CompetencyElement");

            migrationBuilder.DropTable(
                name: "MinisterialCompetency");

            migrationBuilder.DropTable(
                name: "ProgramOfStudies");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
