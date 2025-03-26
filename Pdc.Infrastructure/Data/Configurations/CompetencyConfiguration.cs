using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class CompetencyConfiguration : IEntityTypeConfiguration<Competency>
{
    public void Configure(EntityTypeBuilder<Competency> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.StatementOfCompetency)
            .IsRequired()
            .HasMaxLength(1500);

        builder.HasMany(x => x.RealisationContexts)
            .WithOne()
            .HasForeignKey("CompetencyCode");

        builder.HasMany(x => x.CompetencyElements)
            .WithOne()
            .HasForeignKey("CompetencyId");
    }
}