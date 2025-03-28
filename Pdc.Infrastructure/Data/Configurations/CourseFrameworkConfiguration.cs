using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkConfiguration : IEntityTypeConfiguration<CourseFrameworkEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkEntity> builder)
    {
        builder.HasKey(x => x.CourseCode);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Semester)
            .HasColumnType("TINYINT UNSIGNED")
            .IsRequired();

        builder.Property(x => x.Hours)
            .HasColumnType("TINYINT UNSIGNED")
            .IsRequired();

        builder.Property(x => x.FinalCourseObjective)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.CourseCharacteristics)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.OtherSpecifications)
            .HasMaxLength(5000);

        builder.Property(x => x.StatementOfComplexAuthenticTask)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(x => x.TaskPresentation)
            .HasMaxLength(5000)
            .IsRequired();

        builder.OwnsOne(x => x.Weighting, w =>
        {
            w.Property(p => p.LaboratoryHours)
                .HasColumnType("TINYINT UNSIGNED")
                .IsRequired();
            w.Property(p => p.PersonnalWorkHours)
                .HasColumnType("TINYINT UNSIGNED")
                .IsRequired();
            w.Property(p => p.TheoryHours)
                .HasColumnType("TINYINT UNSIGNED")
                .IsRequired();
        });

        builder.HasOne(p => p.Units)
            .WithMany()
            .HasForeignKey("UnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;

        builder.HasMany(x => x.CourseFrameworkCompetencyElements)
            .WithOne()
            .HasForeignKey("CourseFrameworkCompetencyElementId");

        builder.HasMany(x => x.CourseFrameworkPerformanceCriterias)
            .WithOne()
            .HasForeignKey("CourseFrameworkPerformanceCriteriaId");

        builder.HasMany(x => x.Prerequisites)
            .WithMany();

        builder.HasMany(x => x.AssedElements)
            .WithMany();
    }
}

