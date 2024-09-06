using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class PaginationService
    {
        public PagedResult<T> GetPagedResult<T, TEntity>(
            IQueryable<TEntity> query,
            int pageNumber,
            int pageSize,
            Func<TEntity, T> selector)
        {
            var totalItems = query.Count();
            
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .Select(selector)
                             .ToList();                        

            return new PagedResult<T>(items, totalItems, pageNumber, pageSize);
        }
    }
}
