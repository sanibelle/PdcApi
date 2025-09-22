using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Data.Configurations;
public class CourseFrameworkCompetencyConfiguration : IEntityTypeConfiguration<CourseFrameworkCompetencyEntity>
{
    public void Configure(EntityTypeBuilder<CourseFrameworkCompetencyEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Competency)
            .WithMany()
            .HasForeignKey("CompetencyId")
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Property(x => x.Hours)
            .IsRequired();

        builder.Property(x => x.CompetencyDistribution)
            .HasMaxLength(5000);
    }
}