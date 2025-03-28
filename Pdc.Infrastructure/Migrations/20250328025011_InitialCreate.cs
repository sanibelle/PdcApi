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
            migrationBuilder.CreateSequence(
                name: "ChangeRecordEntitySequence");

            migrationBuilder.CreateTable(
                name: "ChangeRecordEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ChangeRecordEntitySequence]"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ParentVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NextVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRecordEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeRecordEntity_ChangeRecordEntity_NextVersionId",
                        column: x => x.NextVersionId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChangeRecordEntity_ChangeRecordEntity_ParentVersionId",
                        column: x => x.ParentVersionId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WholeUnit = table.Column<int>(type: "int", nullable: false),
                    Numerator = table.Column<int>(type: "int", nullable: true),
                    Denominator = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkEntity",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Weighting_TheoryHours = table.Column<int>(type: "TINYINT UNSIGNED", nullable: false),
                    Weighting_LaboratoryHours = table.Column<int>(type: "TINYINT UNSIGNED", nullable: false),
                    Weighting_PersonnalWorkHours = table.Column<int>(type: "TINYINT UNSIGNED", nullable: false),
                    Semester = table.Column<int>(type: "TINYINT UNSIGNED", nullable: false),
                    Hours = table.Column<int>(type: "TINYINT UNSIGNED", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalCourseObjective = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CourseCharacteristics = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    OtherSpecifications = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    StatementOfComplexAuthenticTask = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    TaskPresentation = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CurrentVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkEntity", x => x.CourseCode);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkEntity_ChangeRecordEntity_CurrentVersionId",
                        column: x => x.CurrentVersionId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkEntity_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfStudies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecificUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OptionnalUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeneralUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplementaryUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sanction = table.Column<int>(type: "int", nullable: false),
                    MonthsDuration = table.Column<int>(type: "int", nullable: false),
                    SpecificDurationHours = table.Column<int>(type: "int", nullable: false),
                    TotalDurationHours = table.Column<int>(type: "int", nullable: false),
                    PublishedOn = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfStudies", x => x.Code);
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
                name: "CourseFrameworkEntityCourseFrameworkEntity",
                columns: table => new
                {
                    CourseFrameworkEntityCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrerequisitesCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkEntityCourseFrameworkEntity", x => new { x.CourseFrameworkEntityCourseCode, x.PrerequisitesCourseCode });
                    table.ForeignKey(
                        name: "FK_CourseFrameworkEntityCourseFrameworkEntity_CourseFrameworkEntity_CourseFrameworkEntityCourseCode",
                        column: x => x.CourseFrameworkEntityCourseCode,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkEntityCourseFrameworkEntity_CourseFrameworkEntity_PrerequisitesCourseCode",
                        column: x => x.PrerequisitesCourseCode,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode");
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfStudyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsOptionnal = table.Column<bool>(type: "bit", nullable: false),
                    StatementOfCompetency = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    CurrentVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Competencies_ChangeRecordEntity_CurrentVersionId",
                        column: x => x.CurrentVersionId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competencies_ProgramOfStudies_ProgramOfStudyCode",
                        column: x => x.ProgramOfStudyCode,
                        principalTable: "ProgramOfStudies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competencies_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkCompetencyEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetencyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseFrameworkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    ReachedTaxonomyLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 6),
                    CompetencyDistribution = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: false),
                    CourseFrameworkCompetencyElementId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkCompetencyEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyEntity_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyEntity_CourseFrameworkEntity_CourseFrameworkCompetencyElementId",
                        column: x => x.CourseFrameworkCompetencyElementId,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode");
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyEntity_CourseFrameworkEntity_CourseFrameworkId",
                        column: x => x.CourseFrameworkId,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeableEntityCourseFrameworkEntity",
                columns: table => new
                {
                    AssedElementsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseFrameworkEntityCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeableEntityCourseFrameworkEntity", x => new { x.AssedElementsId, x.CourseFrameworkEntityCourseCode });
                    table.ForeignKey(
                        name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntity_CourseFrameworkEntityCourseCode",
                        column: x => x.CourseFrameworkEntityCourseCode,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Changeables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TeachedLevel = table.Column<int>(type: "int", nullable: true),
                    CourseFrameworkPerformanceCriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: true),
                    CourseFrameworkPerformanceCriteriaEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompetencyElementEntity_Position = table.Column<int>(type: "int", nullable: true),
                    CompetencyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    CompetencyCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changeables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Changeables_Competencies_CompetencyCode",
                        column: x => x.CompetencyCode,
                        principalTable: "Competencies",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Changeables_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "ChangeDetailEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeType = table.Column<int>(type: "int", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ChangeRecordEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeDetailEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeDetailEntity_ChangeRecordEntity_ChangeRecordEntityId",
                        column: x => x.ChangeRecordEntityId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChangeDetailEntity_ChangeRecordEntity_ChangeRecordId",
                        column: x => x.ChangeRecordId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeDetailEntity_Changeables_ChangeableId",
                        column: x => x.ChangeableId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplementaryInformationEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ChangeRecordEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChangeableEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementaryInformationEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformationEntity_ChangeRecordEntity_ChangeRecordEntityId",
                        column: x => x.ChangeRecordEntityId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComplementaryInformationEntity_ChangeRecordEntity_ChangeRecordId",
                        column: x => x.ChangeRecordId,
                        principalTable: "ChangeRecordEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformationEntity_Changeables_ChangeableEntityId",
                        column: x => x.ChangeableEntityId,
                        principalTable: "Changeables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComplementaryInformationEntity_Changeables_ChangeableId",
                        column: x => x.ChangeableId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkPerformanceCriteriaEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformanceCriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseFrameworkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeachedLevel = table.Column<int>(type: "int", nullable: false),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: false),
                    CourseFrameworkPerformanceCriteriaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkPerformanceCriteriaEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriteriaEntity_Changeables_PerformanceCriteriaId",
                        column: x => x.PerformanceCriteriaId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriteriaEntity_CourseFrameworkEntity_CourseFrameworkId",
                        column: x => x.CourseFrameworkId,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriteriaEntity_CourseFrameworkEntity_CourseFrameworkPerformanceCriteriaId",
                        column: x => x.CourseFrameworkPerformanceCriteriaId,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode");
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkPerformanceEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetencyElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseFrameworkId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    ReachedTaxonomyLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 6),
                    TeachedLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkPerformanceEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceEntity_Changeables_CompetencyElementId",
                        column: x => x.CompetencyElementId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceEntity_CourseFrameworkEntity_CourseFrameworkId",
                        column: x => x.CourseFrameworkId,
                        principalTable: "CourseFrameworkEntity",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntityCourseCode",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "CourseFrameworkEntityCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_Changeables_CompetencyCode",
                table: "Changeables",
                column: "CompetencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Changeables_CompetencyId",
                table: "Changeables",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Changeables_CourseFrameworkPerformanceCriteriaEntityId",
                table: "Changeables",
                column: "CourseFrameworkPerformanceCriteriaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Changeables_CourseFrameworkPerformanceCriteriaId",
                table: "Changeables",
                column: "CourseFrameworkPerformanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetailEntity_ChangeableId",
                table: "ChangeDetailEntity",
                column: "ChangeableId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetailEntity_ChangeRecordEntityId",
                table: "ChangeDetailEntity",
                column: "ChangeRecordEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetailEntity_ChangeRecordId",
                table: "ChangeDetailEntity",
                column: "ChangeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecordEntity_NextVersionId",
                table: "ChangeRecordEntity",
                column: "NextVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecordEntity_ParentVersionId",
                table: "ChangeRecordEntity",
                column: "ParentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_CurrentVersionId",
                table: "Competencies",
                column: "CurrentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_ProgramOfStudyCode",
                table: "Competencies",
                column: "ProgramOfStudyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_UnitsId",
                table: "Competencies",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformationEntity_ChangeableEntityId",
                table: "ComplementaryInformationEntity",
                column: "ChangeableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformationEntity_ChangeableId",
                table: "ComplementaryInformationEntity",
                column: "ChangeableId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformationEntity_ChangeRecordEntityId",
                table: "ComplementaryInformationEntity",
                column: "ChangeRecordEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformationEntity_ChangeRecordId",
                table: "ComplementaryInformationEntity",
                column: "ChangeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencyEntity_CompetencyId",
                table: "CourseFrameworkCompetencyEntity",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencyEntity_CourseFrameworkCompetencyElementId",
                table: "CourseFrameworkCompetencyEntity",
                column: "CourseFrameworkCompetencyElementId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencyEntity_CourseFrameworkId",
                table: "CourseFrameworkCompetencyEntity",
                column: "CourseFrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkEntity_CurrentVersionId",
                table: "CourseFrameworkEntity",
                column: "CurrentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkEntity_UnitsId",
                table: "CourseFrameworkEntity",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkEntityCourseFrameworkEntity_PrerequisitesCourseCode",
                table: "CourseFrameworkEntityCourseFrameworkEntity",
                column: "PrerequisitesCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriteriaEntity_CourseFrameworkId",
                table: "CourseFrameworkPerformanceCriteriaEntity",
                column: "CourseFrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriteriaEntity_CourseFrameworkPerformanceCriteriaId",
                table: "CourseFrameworkPerformanceCriteriaEntity",
                column: "CourseFrameworkPerformanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriteriaEntity_PerformanceCriteriaId",
                table: "CourseFrameworkPerformanceCriteriaEntity",
                column: "PerformanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceEntity_CompetencyElementId",
                table: "CourseFrameworkPerformanceEntity",
                column: "CompetencyElementId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceEntity_CourseFrameworkId",
                table: "CourseFrameworkPerformanceEntity",
                column: "CourseFrameworkId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeableEntityCourseFrameworkEntity_Changeables_AssedElementsId",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "AssedElementsId",
                principalTable: "Changeables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Changeables_CourseFrameworkPerformanceCriteriaEntity_CourseFrameworkPerformanceCriteriaEntityId",
                table: "Changeables",
                column: "CourseFrameworkPerformanceCriteriaEntityId",
                principalTable: "CourseFrameworkPerformanceCriteriaEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Changeables_CourseFrameworkPerformanceCriteriaEntity_CourseFrameworkPerformanceCriteriaId",
                table: "Changeables",
                column: "CourseFrameworkPerformanceCriteriaId",
                principalTable: "CourseFrameworkPerformanceCriteriaEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFrameworkPerformanceCriteriaEntity_Changeables_PerformanceCriteriaId",
                table: "CourseFrameworkPerformanceCriteriaEntity");

            migrationBuilder.DropTable(
                name: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropTable(
                name: "ChangeDetailEntity");

            migrationBuilder.DropTable(
                name: "ComplementaryInformationEntity");

            migrationBuilder.DropTable(
                name: "CourseFrameworkCompetencyEntity");

            migrationBuilder.DropTable(
                name: "CourseFrameworkEntityCourseFrameworkEntity");

            migrationBuilder.DropTable(
                name: "CourseFrameworkPerformanceEntity");

            migrationBuilder.DropTable(
                name: "Changeables");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "CourseFrameworkPerformanceCriteriaEntity");

            migrationBuilder.DropTable(
                name: "ProgramOfStudies");

            migrationBuilder.DropTable(
                name: "CourseFrameworkEntity");

            migrationBuilder.DropTable(
                name: "ChangeRecordEntity");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropSequence(
                name: "ChangeRecordEntitySequence");
        }
    }
}
