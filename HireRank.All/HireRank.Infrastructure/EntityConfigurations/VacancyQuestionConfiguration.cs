using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireRank.Infrastructure.EntityConfigurations
{
    public class VacancyQuestionConfiguration : IEntityTypeConfiguration<VacancyQuestion>
    {
        public void Configure(EntityTypeBuilder<VacancyQuestion> builder)
        {
            builder.ToTable("vacancy_questions");

            builder
                .HasOne(x => x.Vacancy)
                .WithMany(x => x.VacancyQuestions)
                .HasForeignKey(x => x.VacancyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Question)
                .WithMany(x => x.VacancyQuestions)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
