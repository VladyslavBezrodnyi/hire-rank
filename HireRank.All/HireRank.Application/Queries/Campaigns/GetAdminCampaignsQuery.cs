using HireRank.Application.Filtering;
using HireRank.Common.Extensions;
using HireRank.Core.Entities;
using MediatR;
using System;
using System.Linq.Expressions;

namespace HireRank.Application.Queries.Campaigns
{
    public class GetAdminCampaignsQuery : IRequest<PagedResult<CampaignViewModel>>, IQuery<Campaign>
    {
        public string Name { get; set; }

        public DateTime? StartDateFrom { get; set; }

        public DateTime? StartDateTo { get; set; }

        public DateTime? EndDateFrom { get; set; }

        public DateTime? EndDateTo { get; set; }

        public PagingViewModel Paging { get; set; }

        public SortingViewModel Sorting { get; set; }

        public Expression<Func<Campaign, bool>> GetFilteringPredicate()
        {
            Expression<Func<Campaign, bool>> filteringExpression = null;

            if (!string.IsNullOrEmpty(Name))
            {
                TryAddPredicate(ref filteringExpression, x => x.Name.Contains(Name));
            }

            if (StartDateFrom.HasValue)
            {
                TryAddPredicate(ref filteringExpression, x => x.StartDate >= StartDateFrom.Value);
            }

            if (StartDateTo.HasValue)
            {
                TryAddPredicate(ref filteringExpression, x => x.StartDate <= StartDateTo.Value);
            }

            if (EndDateFrom.HasValue)
            {
                TryAddPredicate(ref filteringExpression, x => x.EndDate >= EndDateFrom.Value);
            }

            if (EndDateTo.HasValue)
            {
                TryAddPredicate(ref filteringExpression, x => x.EndDate <= EndDateTo.Value);
            }

            return filteringExpression;
        }

        private void TryAddPredicate(ref Expression<Func<Campaign, bool>> expression,
                                         Expression<Func<Campaign, bool>> predicate)
        {

            expression = expression != null ? expression.AndAlso(predicate) : predicate;

        }
    }
}
