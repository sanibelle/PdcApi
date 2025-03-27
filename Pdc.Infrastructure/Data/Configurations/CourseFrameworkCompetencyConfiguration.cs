using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkCompetencyConfiguration : IEntityTypeConfiguration<CourseFrameworkCompetencyEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkCompetencyEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Competency)
            .WithMany()
            .HasForeignKey("CompetencyId")
            .IsRequired();

        builder.HasOne(x => x.CourseFramework)
            .WithMany()
            .HasForeignKey("CourseFrameworkId")
            .IsRequired();

        builder.Property(x => x.Hours)
            .IsRequired();

        builder.Property(x => x.ReachedTaxonomyLevel)
            .HasDefaultValue(BloomTaxonomy.Creating)
            .IsRequired();

        builder.Property(x => x.CompetencyDistribution)
            .HasMaxLength(5000);
    }
}