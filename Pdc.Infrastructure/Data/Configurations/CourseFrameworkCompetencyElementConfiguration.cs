using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkCompetencyElementConfiguration : IEntityTypeConfiguration<CourseFrameworkCompetencyElementEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkCompetencyElementEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();


        builder.HasOne(x => x.CompetencyElement)
            .WithMany()
            .HasForeignKey("CompetencyElementId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.CourseFramework)
            .WithMany()
            .HasForeignKey("CourseFrameworkId")
            .OnDelete(DeleteBehavior.NoAction)
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