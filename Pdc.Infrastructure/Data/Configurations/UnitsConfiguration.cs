using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Common;
namespace Pdc.Infrastructure.Data.Configurations;

public class UnitsConfiguration : IEntityTypeConfiguration<Units>
{
    public void Configure(EntityTypeBuilder<Units> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.WholeUnits)
            .IsRequired();
    }
}