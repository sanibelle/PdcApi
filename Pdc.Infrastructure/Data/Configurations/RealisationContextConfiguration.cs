using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

internal class RealisationContextConfiguration : IEntityTypeConfiguration<RealisationContextEntity>
{
    public void Configure(EntityTypeBuilder<RealisationContextEntity> builder)
    {
    }
}