using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class MinisterialRealisationContextConfiguration : IEntityTypeConfiguration<RealisationContextEntity>
{
    public void Configure(EntityTypeBuilder<RealisationContextEntity> builder)
    {
    }
}