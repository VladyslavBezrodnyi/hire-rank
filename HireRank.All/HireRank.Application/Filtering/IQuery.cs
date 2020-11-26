using System;
using System.Linq.Expressions;

namespace HireRank.Application.Filtering
{
    public interface IQuery<TEntity> where TEntity : class
    {
        PagingViewModel Paging { get; set; }

        SortingViewModel Sorting { get; set; }

        Expression<Func<TEntity, bool>> GetFilteringPredicate();
    }
}
