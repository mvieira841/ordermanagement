using OrderManagement.Shared.Enums;
using OrderManagement.Shared.Models;
using System.Linq.Dynamic.Core;

namespace OrderManagement.Shared.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, IEnumerable<Sort> sort)
    {
        if (sort != null && sort.Any())
        {
            var ordering = string.Join(",", sort.Select(s => $"{s.Field} {(s.Direction == OrderDirectionType.Descending ? "desc" : "asc")}"));
            return queryable.OrderBy(ordering);
        }
        return queryable;
    }
}