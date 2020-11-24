using HireRank.Common.Exceptions;
using HireRank.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HireRank.Core.Extensions
{
    public static class IEntityExtensions
    {
        public static async Task<T> WithIdAsync<T>(this IQueryable<T> entities, Guid id) where T : IEntity
        {
            var entity =  await entities.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException(id, typeof(T));
            }

            return entity;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> entities, int pageSize, int pageNumber) where T : IEntity
        {
            return entities.Skip(pageNumber * (pageSize - 1)).Take(pageSize);
        }
    }
}
