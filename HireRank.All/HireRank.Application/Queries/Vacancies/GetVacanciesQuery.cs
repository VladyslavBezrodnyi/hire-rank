using HireRank.Application.Filtering;
using HireRank.Application.ViewModels.Shared;
using HireRank.Common.Extensions;
using HireRank.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetVacanciesQuery : IRequest<PagedResult<VacancyViewModel>>, IQuery<Vacancy>
    {
        public string Title { get; set; }

        public string EmployerCompany { get; set; }

        public List<Guid> CampaignIds { get; set; }

        public PagingViewModel Paging { get; set; }

        public SortingViewModel Sorting { get; set; }

        public Expression<Func<Vacancy, bool>> GetFilteringPredicate()
        {
            Expression<Func<Vacancy, bool>> filteringExpression = null;

            if (!string.IsNullOrEmpty(Title))
            {
                TryAddPredicate(ref filteringExpression, vacancy => vacancy.Title.Contains(Title));
            }

            if (!string.IsNullOrEmpty(EmployerCompany))
            {
                TryAddPredicate(ref filteringExpression, vacancy => vacancy.Employer.CompanyName.Contains(EmployerCompany));
            }

            if (CampaignIds.Any())
            {
                TryAddPredicate(ref filteringExpression, vacancy => CampaignIds.Any(x => vacancy.CampaignId == x));
            }

            return filteringExpression;
        }

        private void TryAddPredicate(ref Expression<Func<Vacancy, bool>> expression,
                                        Expression<Func<Vacancy, bool>> predicate)
        {

            expression = expression != null ? expression.AndAlso(predicate) : predicate;

        }
    }
}
