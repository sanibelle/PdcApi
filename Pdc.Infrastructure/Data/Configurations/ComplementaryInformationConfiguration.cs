using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Data.Configurations;
public class ComplementaryInformationConfiguration : IEntityTypeConfiguration<ComplementaryInformationEntity>
{
    public void Configure(EntityTypeBuilder<ComplementaryInformationEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ModifiedOn)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(Constants.MaxComplementaryInformationsLength);
    }
}