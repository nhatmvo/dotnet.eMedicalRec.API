using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class HeadingSetEntityTypeConfiguration : IEntityTypeConfiguration<HeadingSet>
    {
        public void Configure(EntityTypeBuilder<HeadingSet> builder)
        {
            builder.ToTable("mre_heading_sets");
            builder.HasKey(b => b.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property<bool>("_isDefault")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_default")
                .IsRequired();

            builder.Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(HeadingSet.Headings));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}