using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ChangeableConfiguration : IEntityTypeConfiguration<Changeable>
{
    public void Configure(EntityTypeBuilder<Changeable> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .HasMaxLength(Constants.MaxChangeableLength);
    }
}