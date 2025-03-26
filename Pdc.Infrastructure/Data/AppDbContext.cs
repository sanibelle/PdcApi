using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Entities.Versioning;
namespace Pdc.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProgramOfStudy> ProgramOfStudies { get; set; }
    public DbSet<Competency> Competencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Changeable sera directement intégré dans les classes qui l'utilisent
        modelBuilder.Entity<Changeable>().ToTable("Changeables");
        modelBuilder.Entity<Weighting>().UseTpcMappingStrategy();
        modelBuilder.Entity<CompetencyElement>();


        // If you need additional configuration for abstract classes or TPH inheritance
        //modelBuilder.Entity<ContentElement>()
        //    .HasDiscriminator<string>("ContentElementType")
        //    .HasValue<CourseFrameworkContentElement>(nameof(CourseFrameworkContentElement));

        //modelBuilder.Entity<ContentSpecification>()
        //    .HasDiscriminator<string>("ContentSpecificationType")
        //    .HasValue<CourseFrameworkContentSpecification>(nameof(CourseFrameworkContentSpecification));
    }
}
