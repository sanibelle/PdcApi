using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.CourseFramework;
namespace Pdc.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProgramOfStudy> ProgramOfStudies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

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
