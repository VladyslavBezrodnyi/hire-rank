using HireRank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HireRank.Infrastructure.Configurations
{
    public class StudentVacancyConfiguration : IEntityTypeConfiguration<StudentVacancy>
    {
        public void Configure(EntityTypeBuilder<StudentVacancy> builder)
        {
            builder.ToTable("student_vacancies");

            builder
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentVacancies)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Vacancy)
                .WithMany(x => x.StudentVacancies)
                .HasForeignKey(x => x.VacancyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
