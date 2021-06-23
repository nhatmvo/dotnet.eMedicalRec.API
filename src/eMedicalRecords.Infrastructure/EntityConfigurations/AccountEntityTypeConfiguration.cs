using eMedicalRecords.Infrastructure.Securities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMedicalRecords.Infrastructure.EntityConfigurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("account");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property(b => b.Role)
                .HasColumnName("role")
                .IsRequired();

            builder.Property(b => b.Hash)
                .HasColumnName("hash")
                .IsRequired();

            builder.Property(b => b.Username)
                .HasColumnName("username")
                .IsRequired();

            builder.HasIndex(b => b.Username).IsUnique();

            builder.Property(b => b.Salt)
                .HasColumnName("salt")
                .IsRequired();
        }
    }
}