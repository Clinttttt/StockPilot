using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StockPilot.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> PaginatedAsync<T>(IQueryable<T> data, int pageSize, int pageNumber)
        {
            var totalcount = await data.CountAsync();

            var paginate = data.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PaginatedList<T>
            {
                value = paginate,
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalCount = totalcount        
            };

        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,bool condition, Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;
    }
}
