using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Enums;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkCompetencyElementConfiguration : IEntityTypeConfiguration<CourseFrameworkCompetencyElement>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkCompetencyElement> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.CompetencyElement)
                .WithMany()
                .HasForeignKey("CompetencyElementId")
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

        builder.Property(x => x.TeachedLevel)
            .HasDefaultValue(TeachedLevelType.Teached)
            .IsRequired();
    }
}