using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireRank.Infrastructure.EntityConfigurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.ContactPhone).HasMaxLength(14);
        }
    }
}
