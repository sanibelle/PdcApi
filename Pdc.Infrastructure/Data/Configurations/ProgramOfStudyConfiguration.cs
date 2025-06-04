using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;
namespace Pdc.Infrastructure.Data.Configurations;

public class ProgramOfStudyConfiguration : IEntityTypeConfiguration<ProgramOfStudyEntity>
{
    public void Configure(EntityTypeBuilder<ProgramOfStudyEntity> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ProgramType)
            .IsRequired();

        builder.Property(x => x.MonthsDuration)
            .IsRequired();

        builder.Property(x => x.TotalDurationHours)
            .IsRequired();

        builder.Property(x => x.SpecificDurationHours)
            .IsRequired();

        builder.Property(x => x.PublishedOn)
            .IsRequired();

        builder
            .HasMany(x => x.Competencies)
            .WithOne(c => c.ProgramOfStudy)
            .HasForeignKey("ProgramOfStudyCode");

        // Add these new configurations to fix the cascade delete issue
        builder.HasOne(p => p.GeneralUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudyEntity>("GeneralUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property

        builder.HasOne(p => p.ComplementaryUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudyEntity>("ComplementaryUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;

        builder.HasOne(p => p.SpecificUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudyEntity>("SpecificUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;

        builder.HasOne(p => p.OptionalUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudyEntity>("OptionalUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;
    }
}