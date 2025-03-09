using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Common;
namespace Pdc.Infrastructure.Data.Configurations;

public class PerformanceCriteriasConfiguration : IEntityTypeConfiguration<PerformanceCriteria>
{
    public void Configure(EntityTypeBuilder<PerformanceCriteria> builder)
    {
        builder.ConfigureChangeable(1000);

        builder.Property(x => x.Position)
            .IsRequired();

        builder.HasMany(x => x.ContentSpecifications)
            .WithOne()
            .HasForeignKey("ContentElementId");
    }
}