//public class TodoConfiguration : IEntityTypeConfiguration<Todo>
//{
//    public void Configure(EntityTypeBuilder<Todo> builder)
//    {
//        builder.HasKey(t => t.Id);

//        builder.Property(t => t.Text)
//            .IsRequired()
//            .HasMaxLength(200);

//    }
//}