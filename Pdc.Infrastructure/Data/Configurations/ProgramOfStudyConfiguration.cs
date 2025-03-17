using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.CourseFramework;
namespace Pdc.Infrastructure.Data.Configurations;

public class ProgramOfStudyConfiguration : IEntityTypeConfiguration<ProgramOfStudy>
{
    public void Configure(EntityTypeBuilder<ProgramOfStudy> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Sanction)
            .IsRequired();

        builder.Property(x => x.MonthsDuration)
            .IsRequired();

        builder.Property(x => x.TotalDurationHours)
            .IsRequired();

        builder.Property(x => x.SpecificDurationHours)
            .IsRequired();

        builder.Property(x => x.PublishedOn)
            .IsRequired();

        builder.Property(x => x.PublishedOn)
            .IsRequired();

        builder.HasMany(x => x.Competencies)
            .WithOne()
            .HasForeignKey("ProgramOfStudyId");

        // Add these new configurations to fix the cascade delete issue
        builder.HasOne(p => p.GeneralUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudy>("GeneralUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property

        builder.HasOne(p => p.ComplementaryUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudy>("ComplementaryUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;

        builder.HasOne(p => p.SpecificUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudy>("SpecificUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;

        builder.HasOne(p => p.OptionnalUnits)
            .WithOne()
            .HasForeignKey<ProgramOfStudy>("OptionnalUnitsId")
            .OnDelete(DeleteBehavior.ClientCascade); // Shadow property;
    }
}