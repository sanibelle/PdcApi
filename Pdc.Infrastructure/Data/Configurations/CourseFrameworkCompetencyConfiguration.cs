using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;

public class CourseFrameworkCompetencyConfiguration : IEntityTypeConfiguration<CourseFrameworkCompetencyEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkCompetencyEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Competency)
            .WithMany()
            .HasForeignKey(nameof(CourseFrameworkCompetencyEntity.CompetencyCode))
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.CourseFramework)
            .WithMany(x => x.CourseFrameworkCompetencies)
            .HasForeignKey(nameof(CourseFrameworkCompetencyEntity.CourseFrameworkId))
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasIndex(x => new { x.CourseFrameworkId, x.CompetencyCode }).IsUnique();

        builder.Property(x => x.CompetencyDistribution)
            .HasMaxLength(5000);
    }
}