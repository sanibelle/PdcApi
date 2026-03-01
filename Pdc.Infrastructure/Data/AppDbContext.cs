using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;
namespace Pdc.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<IdentityUserEntity, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UnitsEntity> Units { get; set; }
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
        base.OnModelCreating(modelBuilder); // used for IdentityDbContext configuration

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // C'est ce qui permet de mapper Changeables dans sa propre table.
        modelBuilder.Entity<ChangeableEntity>().ToTable("Changeables").UseTptMappingStrategy();

        modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        });
        modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.HasKey(r => new { r.UserId, r.RoleId });
        });
        modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        });
    }
}
