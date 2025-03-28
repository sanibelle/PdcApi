//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Pdc.Domain.Entities.Common;
//namespace Pdc.Infrastructure.Data.Configurations;

//public class WeightingConfiguration : IEntityTypeConfiguration<Weighting>
//{
//    public void Configure(EntityTypeBuilder<Weighting> builder)
//    {

//        builder.Property(x => x.LaboratoryHours)
//            .IsRequired();

//        builder.Property(x => x.PersonnalWorkHours)
//            .IsRequired();

//        builder.Property(x => x.TheoryHours)
//            .IsRequired();
//    }
//}