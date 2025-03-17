using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Versioning;

namespace Pdc.Infrastructure.Data.Configurations;

public static class Helper
{
    public static void ConfigureChangeable<T>(this EntityTypeBuilder<T> builder, int descriptionMaxLength)
    where T : AChangeable
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(descriptionMaxLength);
    }
}
