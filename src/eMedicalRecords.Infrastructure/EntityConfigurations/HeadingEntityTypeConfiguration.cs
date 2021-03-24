using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class HeadingEntityTypeConfiguration : IEntityTypeConfiguration<Heading>
    {
        public void Configure(EntityTypeBuilder<Heading> builder)
        {
            builder.ToTable("mre_headings");
            
            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);
            
            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();
            
            builder.Property<string>("_description")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("description")
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Heading.RecordAttributes));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}