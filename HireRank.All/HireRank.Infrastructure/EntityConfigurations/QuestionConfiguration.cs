using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireRank.Infrastructure.EntityConfigurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("questions");

            builder.HasOne(x => x.Employer)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
