using System;

namespace HireRank.Application.Filters
{
    public interface IFilter<T> where T : class
    {
        Func<T, bool> GetFilterPredicate();

        int PageSize { get; set; }

        int PageNumber { get; set; }
    }
}
