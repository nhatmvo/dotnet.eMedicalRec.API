
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    using Domain.AggregatesModel.TemplateAggregate;

    public class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("mre_template");
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

            var navigation = builder.Metadata.FindNavigation(nameof(Template.Sections));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}