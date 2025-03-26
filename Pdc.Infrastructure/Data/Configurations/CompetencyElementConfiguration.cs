using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Data.Configurations;
public class CompetencyElementConfiguration : IEntityTypeConfiguration<CompetencyElement>
{
    public void Configure(EntityTypeBuilder<CompetencyElement> builder)
    {
        builder.ConfigureChangeable(1000);

        builder.Property(x => x.Position)
            .IsRequired();
    }
}