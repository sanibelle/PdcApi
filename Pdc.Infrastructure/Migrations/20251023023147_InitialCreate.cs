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
                name: "Changeables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changeables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfStudies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecificUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OptionalUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeneralUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplementaryUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProgramType = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_ProgramOfStudies_Units_OptionalUnitsId",
                        column: x => x.OptionalUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramOfStudies_Units_SpecificUnitsId",
                        column: x => x.SpecificUnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChangeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ParentVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NextVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeRecords_ChangeRecords_NextVersionId",
                        column: x => x.NextVersionId,
                        principalTable: "ChangeRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChangeRecords_ChangeRecords_ParentVersionId",
                        column: x => x.ParentVersionId,
                        principalTable: "ChangeRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChangeRecords_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeRecords_Users_ValidatedById",
                        column: x => x.ValidatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChangeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeType = table.Column<int>(type: "int", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeDetails_ChangeRecords_ChangeRecordId",
                        column: x => x.ChangeRecordId,
                        principalTable: "ChangeRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeDetails_Changeables_ChangeableId",
                        column: x => x.ChangeableId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfStudyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    StatementOfCompetency = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    CurrentVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Competencies_ChangeRecords_CurrentVersionId",
                        column: x => x.CurrentVersionId,
                        principalTable: "ChangeRecords",
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
                name: "ComplementaryInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WrittenOnVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementaryInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformations_ChangeRecords_WrittenOnVersionId",
                        column: x => x.WrittenOnVersionId,
                        principalTable: "ChangeRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformations_Changeables_ChangeableId",
                        column: x => x.ChangeableId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformations_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworks",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Weighting_TheoryHours = table.Column<byte>(type: "tinyint", nullable: false),
                    Weighting_LaboratoryHours = table.Column<byte>(type: "tinyint", nullable: false),
                    Weighting_PersonnalWorkHours = table.Column<byte>(type: "tinyint", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false),
                    Hours = table.Column<byte>(type: "tinyint", nullable: false),
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
                    table.PrimaryKey("PK_CourseFrameworks", x => x.CourseCode);
                    table.ForeignKey(
                        name: "FK_CourseFrameworks_ChangeRecords_CurrentVersionId",
                        column: x => x.CurrentVersionId,
                        principalTable: "ChangeRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworks_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompetencyElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CompetencyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencyElements_Changeables_Id",
                        column: x => x.Id,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyElements_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "RealisationContexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetencyCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealisationContexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealisationContexts_Changeables_Id",
                        column: x => x.Id,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealisationContexts_Competencies_CompetencyCode",
                        column: x => x.CompetencyCode,
                        principalTable: "Competencies",
                        principalColumn: "Code",
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
                        name: "FK_ChangeableEntityCourseFrameworkEntity_Changeables_AssedElementsId",
                        column: x => x.AssedElementsId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworks_CourseFrameworkEntityCourseCode",
                        column: x => x.CourseFrameworkEntityCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkCompetencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetencyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseFrameworkCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    CompetencyDistribution = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkCompetencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalTable: "Competencies",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFrameworkCourseCode",
                        column: x => x.CourseFrameworkCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkPrerequisites",
                columns: table => new
                {
                    CourseFrameworkEntityCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrerequisitesCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkPrerequisites", x => new { x.CourseFrameworkEntityCourseCode, x.PrerequisitesCourseCode });
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_CourseFrameworkEntityCourseCode",
                        column: x => x.CourseFrameworkEntityCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_PrerequisitesCourseCode",
                        column: x => x.PrerequisitesCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode");
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkCompetencyElements",
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
                    table.PrimaryKey("PK_CourseFrameworkCompetencyElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyElements_CompetencyElements_CompetencyElementId",
                        column: x => x.CompetencyElementId,
                        principalTable: "CompetencyElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyElements_CourseFrameworks_CourseFrameworkId",
                        column: x => x.CourseFrameworkId,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CompetencyElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceCriterias_Changeables_Id",
                        column: x => x.Id,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerformanceCriterias_CompetencyElements_CompetencyElementId",
                        column: x => x.CompetencyElementId,
                        principalTable: "CompetencyElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkPerformanceCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformanceCriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseFrameworkCourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeachedLevel = table.Column<int>(type: "int", nullable: false),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkPerformanceCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_CourseFrameworkCourseCode",
                        column: x => x.CourseFrameworkCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriterias_PerformanceCriterias_PerformanceCriteriaId",
                        column: x => x.PerformanceCriteriaId,
                        principalTable: "PerformanceCriterias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContentElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeachedLevel = table.Column<int>(type: "int", nullable: false),
                    CourseFrameworkPerformanceCriteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAssedElement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentElements_Changeables_Id",
                        column: x => x.Id,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentElements_CourseFrameworkPerformanceCriterias_CourseFrameworkPerformanceCriteriaId",
                        column: x => x.CourseFrameworkPerformanceCriteriaId,
                        principalTable: "CourseFrameworkPerformanceCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntityCourseCode",
                table: "ChangeableEntityCourseFrameworkEntity",
                column: "CourseFrameworkEntityCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetails_ChangeableId",
                table: "ChangeDetails",
                column: "ChangeableId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetails_ChangeRecordId",
                table: "ChangeDetails",
                column: "ChangeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_CreatedById",
                table: "ChangeRecords",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_NextVersionId",
                table: "ChangeRecords",
                column: "NextVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_ParentVersionId",
                table: "ChangeRecords",
                column: "ParentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeRecords_ValidatedById",
                table: "ChangeRecords",
                column: "ValidatedById");

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
                name: "IX_CompetencyElements_CompetencyId",
                table: "CompetencyElements",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformations_ChangeableId",
                table: "ComplementaryInformations",
                column: "ChangeableId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformations_CreatedById",
                table: "ComplementaryInformations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryInformations_WrittenOnVersionId",
                table: "ComplementaryInformations",
                column: "WrittenOnVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentElements_CourseFrameworkPerformanceCriteriaId",
                table: "ContentElements",
                column: "CourseFrameworkPerformanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencies_CompetencyId",
                table: "CourseFrameworkCompetencies",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencies_CourseFrameworkCourseCode",
                table: "CourseFrameworkCompetencies",
                column: "CourseFrameworkCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencyElements_CompetencyElementId",
                table: "CourseFrameworkCompetencyElements",
                column: "CompetencyElementId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkCompetencyElements_CourseFrameworkId",
                table: "CourseFrameworkCompetencyElements",
                column: "CourseFrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCourseCode",
                table: "CourseFrameworkPerformanceCriterias",
                column: "CourseFrameworkCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPerformanceCriterias_PerformanceCriteriaId",
                table: "CourseFrameworkPerformanceCriterias",
                column: "PerformanceCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworkPrerequisites_PrerequisitesCourseCode",
                table: "CourseFrameworkPrerequisites",
                column: "PrerequisitesCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_CurrentVersionId",
                table: "CourseFrameworks",
                column: "CurrentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFrameworks_UnitsId",
                table: "CourseFrameworks",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceCriterias_CompetencyElementId",
                table: "PerformanceCriterias",
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
                name: "IX_ProgramOfStudies_OptionalUnitsId",
                table: "ProgramOfStudies",
                column: "OptionalUnitsId",
                unique: true,
                filter: "[OptionalUnitsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_SpecificUnitsId",
                table: "ProgramOfStudies",
                column: "SpecificUnitsId",
                unique: true,
                filter: "[SpecificUnitsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RealisationContexts_CompetencyCode",
                table: "RealisationContexts",
                column: "CompetencyCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeableEntityCourseFrameworkEntity");

            migrationBuilder.DropTable(
                name: "ChangeDetails");

            migrationBuilder.DropTable(
                name: "ComplementaryInformations");

            migrationBuilder.DropTable(
                name: "ContentElements");

            migrationBuilder.DropTable(
                name: "CourseFrameworkCompetencies");

            migrationBuilder.DropTable(
                name: "CourseFrameworkCompetencyElements");

            migrationBuilder.DropTable(
                name: "CourseFrameworkPrerequisites");

            migrationBuilder.DropTable(
                name: "RealisationContexts");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "CourseFrameworkPerformanceCriterias");

            migrationBuilder.DropTable(
                name: "CourseFrameworks");

            migrationBuilder.DropTable(
                name: "PerformanceCriterias");

            migrationBuilder.DropTable(
                name: "CompetencyElements");

            migrationBuilder.DropTable(
                name: "Changeables");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "ChangeRecords");

            migrationBuilder.DropTable(
                name: "ProgramOfStudies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
