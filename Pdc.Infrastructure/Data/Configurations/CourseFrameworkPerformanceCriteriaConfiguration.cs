using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkPerformanceCriteriaConfiguration : IEntityTypeConfiguration<CourseFrameworkPerformanceCriteriaEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkPerformanceCriteriaEntity> builder)
    {
        builder.HasKey(x => x.Id);

        //public required TeachedLevelType TeachedLevel { get; set; }
        //public bool IsAssedElement { get; set; } = false;

        builder.HasOne(x => x.PerformanceCriteria)
            .WithMany()
            .HasForeignKey("PerformanceCriteriaId")
            .IsRequired();

        builder.HasOne(x => x.CourseFramework)
            .WithMany()
            .HasForeignKey("CourseFrameworkId")
            .IsRequired();

        builder.HasMany(x => x.ContentElements);
    }
}