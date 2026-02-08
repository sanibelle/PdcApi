using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Changeables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changeables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WholeUnit = table.Column<int>(type: "integer", nullable: false),
                    Numerator = table.Column<int>(type: "integer", nullable: true),
                    Denominator = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    ParentVersionId = table.Column<Guid>(type: "uuid", nullable: true),
                    NextVersionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ValidatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    ValidatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeRecords_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeRecords_AspNetUsers_ValidatedById",
                        column: x => x.ValidatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfStudies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    SpecificUnitsId = table.Column<Guid>(type: "uuid", nullable: true),
                    OptionalUnitsId = table.Column<Guid>(type: "uuid", nullable: true),
                    GeneralUnitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComplementaryUnitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProgramType = table.Column<int>(type: "integer", nullable: false),
                    MonthsDuration = table.Column<int>(type: "integer", nullable: false),
                    SpecificDurationHours = table.Column<int>(type: "integer", nullable: false),
                    TotalDurationHours = table.Column<int>(type: "integer", nullable: false),
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
                name: "ChangeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeableId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeType = table.Column<int>(type: "integer", nullable: false),
                    OldValue = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true)
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
                name: "ComplementaryInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangeableId = table.Column<Guid>(type: "uuid", nullable: false),
                    WrittenOnVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementaryInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplementaryInformations_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworks",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Weighting_TheoryHours = table.Column<short>(type: "smallint", nullable: false),
                    Weighting_LaboratoryHours = table.Column<short>(type: "smallint", nullable: false),
                    Weighting_PersonnalWorkHours = table.Column<short>(type: "smallint", nullable: false),
                    Semester = table.Column<short>(type: "smallint", nullable: false),
                    Hours = table.Column<short>(type: "smallint", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FinalCourseObjective = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    CourseCharacteristics = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    OtherSpecifications = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    StatementOfComplexAuthenticTask = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    TaskPresentation = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    CurrentVersionId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "Competencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProgramOfStudyCode = table.Column<string>(type: "text", nullable: false),
                    IsMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    IsOptional = table.Column<bool>(type: "boolean", nullable: false),
                    StatementOfCompetency = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: false),
                    CurrentVersionId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "ChangeableEntityCourseFrameworkEntity",
                columns: table => new
                {
                    AssedElementsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseFrameworkEntityCourseCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeableEntityCourseFrameworkEntity", x => new { x.AssedElementsId, x.CourseFrameworkEntityCourseCode });
                    table.ForeignKey(
                        name: "FK_ChangeableEntityCourseFrameworkEntity_Changeables_AssedElem~",
                        column: x => x.AssedElementsId,
                        principalTable: "Changeables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeableEntityCourseFrameworkEntity_CourseFrameworks_Cour~",
                        column: x => x.CourseFrameworkEntityCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseFrameworkPrerequisites",
                columns: table => new
                {
                    CourseFrameworkEntityCourseCode = table.Column<string>(type: "text", nullable: false),
                    PrerequisitesCourseCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkPrerequisites", x => new { x.CourseFrameworkEntityCourseCode, x.PrerequisitesCourseCode });
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_CourseFramewo~",
                        column: x => x.CourseFrameworkEntityCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPrerequisites_CourseFrameworks_Prerequisites~",
                        column: x => x.PrerequisitesCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    CompetencyId = table.Column<string>(type: "text", nullable: true)
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
                name: "CourseFrameworkCompetencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetencyId = table.Column<string>(type: "text", nullable: false),
                    CourseFrameworkCourseCode = table.Column<string>(type: "text", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
                    CompetencyDistribution = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    IsAssedElement = table.Column<bool>(type: "boolean", nullable: false)
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
                        name: "FK_CourseFrameworkCompetencies_CourseFrameworks_CourseFramewor~",
                        column: x => x.CourseFrameworkCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealisationContexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetencyCode = table.Column<string>(type: "text", nullable: false)
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
                name: "CourseFrameworkCompetencyElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompetencyElementId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseFrameworkId = table.Column<string>(type: "text", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
                    ReachedTaxonomyLevel = table.Column<int>(type: "integer", nullable: false, defaultValue: 6),
                    TeachedLevel = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    IsAssedElement = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkCompetencyElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyElements_CompetencyElements_Compet~",
                        column: x => x.CompetencyElementId,
                        principalTable: "CompetencyElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseFrameworkCompetencyElements_CourseFrameworks_CourseFr~",
                        column: x => x.CourseFrameworkId,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    CompetencyElementId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PerformanceCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseFrameworkCourseCode = table.Column<string>(type: "text", nullable: false),
                    TeachedLevel = table.Column<int>(type: "integer", nullable: false),
                    IsAssedElement = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFrameworkPerformanceCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriterias_CourseFrameworks_Course~",
                        column: x => x.CourseFrameworkCourseCode,
                        principalTable: "CourseFrameworks",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseFrameworkPerformanceCriterias_PerformanceCriterias_Pe~",
                        column: x => x.PerformanceCriteriaId,
                        principalTable: "PerformanceCriterias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContentElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeachedLevel = table.Column<int>(type: "integer", nullable: false),
                    CourseFrameworkPerformanceCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsAssedElement = table.Column<bool>(type: "boolean", nullable: false)
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
                        name: "FK_ContentElements_CourseFrameworkPerformanceCriterias_CourseF~",
                        column: x => x.CourseFrameworkPerformanceCriteriaId,
                        principalTable: "CourseFrameworkPerformanceCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeableEntityCourseFrameworkEntity_CourseFrameworkEntity~",
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
                name: "IX_CourseFrameworkPerformanceCriterias_CourseFrameworkCourseCo~",
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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfStudies_SpecificUnitsId",
                table: "ProgramOfStudies",
                column: "SpecificUnitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RealisationContexts_CompetencyCode",
                table: "RealisationContexts",
                column: "CompetencyCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "AspNetRoles");

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
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
