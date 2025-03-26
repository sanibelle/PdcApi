using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class PerformanceCriteriasConfiguration : IEntityTypeConfiguration<PerformanceCriteria>
{
    public void Configure(EntityTypeBuilder<PerformanceCriteria> builder)
    {
        builder.Property(x => x.Position)
            .IsRequired();
    }
}