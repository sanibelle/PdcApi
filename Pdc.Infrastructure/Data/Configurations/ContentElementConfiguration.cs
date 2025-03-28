using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;
namespace Pdc.Infrastructure.Data.Configurations;

public class ContentElementConfiguration : IEntityTypeConfiguration<ContentElementEntity>
{
    public void Configure(EntityTypeBuilder<ContentElementEntity> builder)
    {
        builder.HasOne(x => x.CourseFrameworkPerformanceCriteria)
            .WithMany()
            .HasForeignKey("CourseFrameworkPerformanceCriteriaId")
            .IsRequired();
    }
}