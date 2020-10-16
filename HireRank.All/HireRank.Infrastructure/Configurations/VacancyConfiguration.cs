using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireRank.Infrastructure.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("vacancies");

            builder
                .HasOne(x => x.Campaign)
                .WithMany(x => x.Vacancies)
                .HasForeignKey(x => x.CampaignId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Employer)
                .WithMany(x => x.Vacancies)
                .HasForeignKey(x => x.EmployerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
