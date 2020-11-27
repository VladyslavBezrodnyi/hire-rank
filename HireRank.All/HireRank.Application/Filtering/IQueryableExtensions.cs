using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace HireRank.Application.Filtering
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResult<TViewModel>> ApplyQueryAsync<T, TViewModel>(this IQueryable<T> entities,
                                                                                    IQuery<T> query,
                                                                                    IConfigurationProvider mappingProvider) where T : class
        {
            var filteredEntities = entities;
            var filter = query.GetFilteringPredicate();

            if (filter != null)
            {
                filteredEntities = filteredEntities.Where(filter);
            }

            var totalCount = await filteredEntities.CountAsync();

            if (query.Sorting != null)
            {
                filteredEntities = filteredEntities.ApplySorting(query.Sorting);
            }

            if (query.Paging != null)
            {
                filteredEntities = filteredEntities.ApplyPaging(query.Paging);
            }

            var result = await filteredEntities.ProjectTo<TViewModel>(mappingProvider).ToListAsync();

            return new PagedResult<TViewModel>()
            {
                TotalCount = totalCount,
                Items = result
            };
        } 

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> entities, SortingViewModel sorting)
        {
            if (string.IsNullOrEmpty(sorting.SortingProperty))
            {
                return entities;
            }

            var sortingProperty = TypeDescriptor.GetProperties(typeof(T)).Find(sorting.SortingProperty, true);

            if (sortingProperty == null)
            {
                return entities;
            }

            return entities.OrderBy($"{sorting.SortingProperty} {sorting.SortingOrder}");
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> entities, PagingViewModel paging)
        {
            return entities.Skip(paging.PageSize * (paging.PageNumber - 1)).Take(paging.PageSize);
        }
    }
}
