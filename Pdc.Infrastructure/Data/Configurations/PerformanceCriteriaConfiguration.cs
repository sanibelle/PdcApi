using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class PerformanceCriteriasConfiguration : IEntityTypeConfiguration<PerformanceCriteriaEntity>
{
    public void Configure(EntityTypeBuilder<PerformanceCriteriaEntity> builder)
    {
        builder.Property(x => x.Position)
            .IsRequired();
    }
}