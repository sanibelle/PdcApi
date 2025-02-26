using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Common;
using Pdc.Infrastructure.Data.Configurations;

public class ContentSpecificationConfiguration : IEntityTypeConfiguration<ContentSpecification>
{
    public void Configure(EntityTypeBuilder<ContentSpecification> builder)
    {
        builder.ConfigureChangeable(1000);
    }
}