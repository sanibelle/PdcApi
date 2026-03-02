using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class UnitsConfiguration : IEntityTypeConfiguration<UnitsEntity>
{
    public void Configure(EntityTypeBuilder<UnitsEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.WholeUnit)
            .IsRequired();
    }
}