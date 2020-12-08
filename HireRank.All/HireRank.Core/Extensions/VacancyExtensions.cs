using HireRank.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireRank.Core.Extensions
{
    public static class VacancyExtensions
    {
        public static IQueryable<Vacancy> ActiveVacancies(this IQueryable<Vacancy> vacancies)
        {
            return vacancies.Where(vacancy => vacancy.Campaign.EndDate > DateTime.Now && vacancy.Campaign.StartDate <= DateTime.Now);
        }
    }
}
