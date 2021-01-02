using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireRank.Infrastructure.EntityConfigurations
{
    public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder
                .Property(x => x.CompanyName)
                .HasMaxLength(255);

            builder
                .Property(x => x.CompanyDescription)
                .HasMaxLength(255);

            builder
                .Property(x => x.CompanyDescription)
                .HasMaxLength(255);

            builder
                .Property(x => x.ContactPhoneNumber)
                .HasMaxLength(14);

            builder
                .Property(x => x.IsConfirmed)
                .IsRequired()
                .HasDefaultValue<bool>(false);
        }
    }
}
