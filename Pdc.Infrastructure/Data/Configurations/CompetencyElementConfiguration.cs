using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Data.Configurations;
public class CompetencyElementConfiguration : IEntityTypeConfiguration<CompetencyElementEntity>
{
    public void Configure(EntityTypeBuilder<CompetencyElementEntity> builder)
    {
        builder.Property(x => x.Position)
            .IsRequired();
    }
}