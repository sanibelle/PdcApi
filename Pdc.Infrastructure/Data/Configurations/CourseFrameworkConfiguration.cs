using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;

public class CourseFrameworkConfiguration : IEntityTypeConfiguration<CourseFrameworkEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.ProgramOfStudy)
            .WithMany()
            .HasForeignKey(x => x.ProgramOfStudyCode)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(x => x.Name)
            .WithOne()
            .HasForeignKey<CourseFrameworkEntity>("NameId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.Code)
            .WithOne()
            .HasForeignKey<CourseFrameworkEntity>("CodeId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.Semester)
            .WithOne()
            .HasForeignKey<CourseFrameworkEntity>("SemesterId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(x => x.FinalCourseObjective)
            .HasMaxLength(5000)
            .IsRequired(false);

        builder.Property(x => x.CourseCharacteristics)
            .HasMaxLength(5000)
            .IsRequired(false);


        builder.Property(x => x.OtherSpecifications)
            .HasMaxLength(5000)
            .IsRequired(false);

        builder.Property(x => x.StatementOfComplexAuthenticTask)
            .HasMaxLength(5000)
            .IsRequired(false);

        builder.Property(x => x.TaskPresentation)
            .HasMaxLength(5000)
            .IsRequired(false);

        builder.HasOne(x => x.PersonnalWorkHours)
            .WithOne()
            .HasForeignKey<CourseFrameworkEntity>("PersonnalWorkHoursId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.LaboratoryHours)
            .WithOne()
            .HasForeignKey<CourseFrameworkEntity>("LaboratoryHoursId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.TheoryHours)
            .WithOne()
            .HasForeignKey<CourseFrameworkEntity>("TheoryHoursId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Prerequisites)
            .WithMany()
            .UsingEntity(x => x.ToTable("CourseFrameworkPrerequisites"));

        builder.HasMany(x => x.AssedElements)
            .WithMany();
    }
}

