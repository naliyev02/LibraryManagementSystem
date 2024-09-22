using LibraryManagementSystem.Business.DTOs.ExtensionDtos.OrderByDtos;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Business.Extensions;

public static class OrderByExtension
{
    public static async Task<IQueryable<T>> QueryableOrderBy<T>(this IQueryable<T> query, OrderByRequestDto? orderBy)
    {

        if (string.IsNullOrWhiteSpace(orderBy?.ColumnName))
            return query;

        var parametr = Expression.Parameter(typeof(T), "x");

        var property = Expression.Property(parametr, orderBy.ColumnName);

        var lambda = Expression.Lambda(property, parametr);

        var methodName = orderBy.Ascending ? "OrderBy" : "OrderByDescending";

        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression,
            Expression.Quote(lambda)
        );

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}
