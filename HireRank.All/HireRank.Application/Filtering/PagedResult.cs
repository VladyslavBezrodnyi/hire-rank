using System.Collections.Generic;

namespace HireRank.Application.Filtering
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
