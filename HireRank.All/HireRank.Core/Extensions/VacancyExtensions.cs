using HireRank.Core.Entities;
using System;
using System.Linq;

namespace HireRank.Core.Extensions
{
    public static class VacancyExtensions
    {
        public static IQueryable<Vacancy> Active(this IQueryable<Vacancy> vacancies)
        {
            return vacancies.Where(vacancy => vacancy.Campaign.EndDate > DateTime.Now
                                            && vacancy.Campaign.StartDate <= DateTime.Now);
        }

        public static IQueryable<Vacancy> WithTestBase(this IQueryable<Vacancy> vacancies)
        {
            return vacancies.Where(vacancy => vacancy.VacancyQuestions.Any());
        }
    }
}
