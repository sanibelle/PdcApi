using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class MinisterialCompetencyConfiguration : IEntityTypeConfiguration<MinisterialCompetency>
{
    public void Configure(EntityTypeBuilder<MinisterialCompetency> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(4);

        builder.Property(x => x.StatementOfCompetency)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(x => x.RealisationContexts)
            .WithOne()
            .HasForeignKey("MinisterialCompetencyId");

        builder.HasMany(x => x.CompetencyElements)
            .WithOne()
            .HasForeignKey("MinisterialCompetencyId");
    }
}