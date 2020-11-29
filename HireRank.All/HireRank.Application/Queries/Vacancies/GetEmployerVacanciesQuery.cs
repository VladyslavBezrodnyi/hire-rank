using HireRank.Application.Filtering;
using HireRank.Application.ViewModels.Shared;
using HireRank.Common.Extensions;
using HireRank.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HireRank.Application.Queries.Vacancies
{
    public class GetEmployerVacanciesQuery : IRequest<PagedResult<VacancyViewModel>>, IQuery<Vacancy>
    {
        public string Title { get; set; }

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

            return filteringExpression;
        }

        private void TryAddPredicate(ref Expression<Func<Vacancy, bool>> expression,
                                        Expression<Func<Vacancy, bool>> predicate)
        {

            expression = expression != null ? expression.AndAlso(predicate) : predicate;

        }
    }
}