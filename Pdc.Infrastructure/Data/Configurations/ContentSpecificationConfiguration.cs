using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Common;
namespace Pdc.Infrastructure.Data.Configurations;

public class ContentSpecificationConfiguration : IEntityTypeConfiguration<ComplementaryInformations>
{
    public void Configure(EntityTypeBuilder<ComplementaryInformations> builder)
    {
        builder.ConfigureChangeable(1000);
    }
}