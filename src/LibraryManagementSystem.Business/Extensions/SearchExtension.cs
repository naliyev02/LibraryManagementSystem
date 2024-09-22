using LibraryManagementSystem.Business.DTOs.ExtensionDtos.SearchDtos;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Business.Extensions;

public static class SearchExtension
{
    public static async Task<IQueryable<T>> QueryableSearch<T> (this IQueryable<T> query, List<SearchRequestDto>? searchParams)
    {
        if (searchParams == null || !searchParams.Any())
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression finalExpression = null;

        foreach (var param in searchParams)
        {
            Expression property = parameter;
            foreach (var member in param.ColumnName.Split('.'))
            {
                property = Expression.Property(property, member);
            }

            var propertyType = property.Type;

            Expression comparisonExpression;

            if (propertyType == typeof(string))
            {
                var constant = Expression.Constant(param.Value.ToString());
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                comparisonExpression = Expression.Call(property, containsMethod, constant);
            }
            else
            {
                var constant = Expression.Constant(Convert.ChangeType(param.Value, propertyType));
                comparisonExpression = Expression.Equal(property, constant);
            }
            

            finalExpression = finalExpression == null
                ? comparisonExpression
                : Expression.AndAlso(finalExpression, comparisonExpression);
        }

        var lambda = Expression.Lambda<Func<T, bool>>(finalExpression, parameter);

        return query.Where(lambda);
    }
}
