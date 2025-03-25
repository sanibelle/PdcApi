using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ContentSpecificationConfiguration : IEntityTypeConfiguration<ComplementaryInformation>
{
    public void Configure(EntityTypeBuilder<ComplementaryInformation> builder)
    {
        //builder.ConfigureChangeable(1000);
    }
}