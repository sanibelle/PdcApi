using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;
namespace Pdc.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Units> Units { get; set; }
    public DbSet<ProgramOfStudyEntity> ProgramOfStudies { get; set; }
    public DbSet<CompetencyEntity> Competencies { get; set; }
    public DbSet<CompetencyElementEntity> CompetencyElements { get; set; }
    public DbSet<PerformanceCriteriaEntity> PerformanceCriterias { get; set; }
    public DbSet<ChangeRecordEntity> ChangeRecords { get; set; }
    public DbSet<RealisationContextEntity> RealisationContexts { get; set; }
    public DbSet<ChangeableEntity> Changeables { get; set; }
    public DbSet<ComplementaryInformationEntity> ComplementaryInformations { get; set; }
    public DbSet<CourseFrameworkCompetencyEntity> CourseFrameworkCompetencies { get; set; }
    public DbSet<CourseFrameworkCompetencyElementEntity> CourseFrameworkCompetencyElements { get; set; }
    public DbSet<CourseFrameworkPerformanceCriteriaEntity> CourseFrameworkPerformanceCriterias { get; set; }
    public DbSet<ContentElementEntity> ContentElements { get; set; }
    public DbSet<CourseFrameworkEntity> CourseFrameworks { get; set; }
    public DbSet<ChangeDetailEntity> ChangeDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Changeable sera directement intégré dans les classes qui l'utilisent
        modelBuilder.Entity<ChangeableEntity>().ToTable("Changeables").UseTptMappingStrategy();
        modelBuilder.Entity<CompetencyElementEntity>().ToTable("CompetencyElements").HasBaseType<ChangeableEntity>();
        modelBuilder.Entity<CourseFrameworkCompetencyEntity>().ToTable("CourseFrameworkCompetencies");

        // If you need additional configuration for abstract classes or TPH inheritance
        //modelBuilder.Entity<ContentElement>()
        //    .HasDiscriminator<string>("ContentElementType")
        //    .HasValue<CourseFrameworkContentElement>(nameof(CourseFrameworkContentElement));

        //modelBuilder.Entity<ContentSpecification>()
        //    .HasDiscriminator<string>("ContentSpecificationType")
        //    .HasValue<CourseFrameworkContentSpecification>(nameof(CourseFrameworkContentSpecification));
    }
}
