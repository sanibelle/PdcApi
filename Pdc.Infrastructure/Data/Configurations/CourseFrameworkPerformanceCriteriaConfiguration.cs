using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkPerformanceCriteriaConfiguration : IEntityTypeConfiguration<CourseFrameworkPerformanceCriteriaEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkPerformanceCriteriaEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.PerformanceCriteria)
            .WithMany()
            .HasForeignKey("PerformanceCriteriaId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasMany(x => x.ContentElements);
    }
}