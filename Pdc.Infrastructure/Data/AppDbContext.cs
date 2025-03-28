using Microsoft.EntityFrameworkCore;
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

    public DbSet<ProgramOfStudyEntity> ProgramOfStudies { get; set; }
    public DbSet<CompetencyEntity> Competencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Changeable sera directement intégré dans les classes qui l'utilisent
        modelBuilder.Entity<ChangeableEntity>().ToTable("Changeables");
        modelBuilder.Entity<CompetencyElementEntity>();


        // If you need additional configuration for abstract classes or TPH inheritance
        //modelBuilder.Entity<ContentElement>()
        //    .HasDiscriminator<string>("ContentElementType")
        //    .HasValue<CourseFrameworkContentElement>(nameof(CourseFrameworkContentElement));

        //modelBuilder.Entity<ContentSpecification>()
        //    .HasDiscriminator<string>("ContentSpecificationType")
        //    .HasValue<CourseFrameworkContentSpecification>(nameof(CourseFrameworkContentSpecification));
    }
}
