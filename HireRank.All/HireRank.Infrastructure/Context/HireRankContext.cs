using HireRank.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace HireRank.Infrastructure.Context
{
    public class HireRankContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<StudentVacancy> StudentVacancies { get; set; }

        public DbSet<VacancyQuestion> VacancyQuestions { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Option> Options { get; set; }

        public HireRankContext(DbContextOptions<HireRankContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
