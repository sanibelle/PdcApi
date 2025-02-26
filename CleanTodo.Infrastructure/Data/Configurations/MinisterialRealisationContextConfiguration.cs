using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Data.Configurations;

public class MinisterialRealisationContextConfiguration : IEntityTypeConfiguration<MinisterialRealisationContext>
{
    public void Configure(EntityTypeBuilder<MinisterialRealisationContext> builder)
    {
        builder.ConfigureChangeable(1000);
    }
}