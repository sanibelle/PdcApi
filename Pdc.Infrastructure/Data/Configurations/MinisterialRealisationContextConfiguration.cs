using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class MinisterialRealisationContextConfiguration : IEntityTypeConfiguration<MinisterialRealisationContextEntity>
{
    public void Configure(EntityTypeBuilder<MinisterialRealisationContextEntity> builder)
    {
    }
}