using SharedKarnel.Exceptions;
using System.Linq.Expressions;

namespace SharedKarnel.Grids;

public static class GridOperations
{
    public static IQueryable<T> Search<T>(IQueryable<T> query, List<Search>? searchFilters)
    where T : class
    {
        if (searchFilters is null) return query;

        foreach (var filter in searchFilters)
        {
            query = ApplyFilter(query, filter);
        }

        return query;
    }

    private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, Search search) where T : class
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? combinedExpression = null;

        foreach (var field in search.GetPascalCaseFields)
        {
            // Ensure the field exists on the entity
            var property = typeof(T).GetProperty(field) ??
                           throw new InvalidRequestException($"Invalid search field: '{field}'");

            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;


            object? convertedValue = null;
            try
            {
                if (targetType.IsEnum)
                {
                    convertedValue = string.IsNullOrEmpty(search.Value) ? null : Enum.Parse(targetType, search.Value!, ignoreCase: true);
                }
                else
                {
                    convertedValue = Convert.ChangeType(search.Value, targetType);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidRequestException($"Incompatible search value: '{search.Value}' in field '{field}'");
                //continue;
            }

            Expression? leftExpression = Expression.Property(parameter, property);
            Expression? targetValue = Expression.Constant(convertedValue, property.PropertyType);

            if (property.PropertyType == typeof(string))
            {
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                leftExpression = Expression.Call(leftExpression, toLowerMethod!);
                targetValue = Expression.Call(Expression.Constant(convertedValue), toLowerMethod!);
            }

            // Generate the expression based on the operator
            Expression? filterExpression = search.Operator.ToLower() switch
            {
                "=" => Expression.Equal(leftExpression, targetValue),
                ">" => Expression.GreaterThan(leftExpression, targetValue),
                "<" => Expression.LessThan(leftExpression, targetValue),
                ">=" => Expression.GreaterThanOrEqual(leftExpression, targetValue),
                "<=" => Expression.LessThanOrEqual(leftExpression, targetValue),
                "contains" when property.PropertyType == typeof(string) => Expression.Call(
                    leftExpression, typeof(string).GetMethod("Contains", [typeof(string)])!, targetValue),
                "contains-or-equal" => property.PropertyType switch
                {
                    // String case: check for Contains or Equal
                    Type type when type == typeof(string) => Expression.OrElse(
                        Expression.Call(
                            leftExpression, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, targetValue
                        ),
                        Expression.Equal(leftExpression, targetValue)
                    ),
                    // Integer case: check for Equality
                    Type type when type == typeof(int) || type == typeof(int?) => Expression.Equal(leftExpression, targetValue),

                    // Long case: check for Equality
                    Type type when type == typeof(long) || type == typeof(long?) => Expression.Equal(leftExpression, targetValue),

                    // Decimal case: check if between floor and ceiling values
                    Type type when type == typeof(decimal) || type == typeof(decimal?) => Expression.AndAlso(
                        Expression.GreaterThanOrEqual(
                            leftExpression, Expression.Constant(Math.Floor(Convert.ToDecimal(targetValue)))
                        ),
                        Expression.LessThanOrEqual(
                            leftExpression, Expression.Constant(Math.Ceiling(Convert.ToDecimal(targetValue)))
                        )
                    ),
                    // Datetime case: check if between floor and ceiling values
                    Type type when convertedValue != null && type == typeof(DateTime) => Expression.AndAlso(
                        Expression.GreaterThanOrEqual(
                            leftExpression, Expression.Constant(((DateTime) convertedValue).Date)
                        ),
                        Expression.LessThanOrEqual(
                            leftExpression, Expression.Constant(((DateTime) convertedValue).Date.AddDays(1).AddTicks(-1))
                        )
                    ),

                    _ => null // For unsupported types, return null
                },
                _ => null
            };

            if (filterExpression != null)
            {
                combinedExpression = combinedExpression == null
                    ? filterExpression
                    : Expression.OrElse(combinedExpression, filterExpression);
            }
        }

        if (combinedExpression != null)
        {
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

    public static IOrderedQueryable<T> Sort<T>(IQueryable<T> query, Sort sort)
    {
        if (!Enum.TryParse(sort.Direction, true, out SortOrder direction))
        {
            throw new InvalidRequestException($"Invalid sort direction: '{sort.Direction}'");
        }

        var orderBy = direction == SortOrder.Asc ? "OrderBy" : "OrderByDescending";

        var parameterExpression = Expression.Parameter(typeof(T), string.Empty);
        var memberExpression = Expression.PropertyOrField(parameterExpression, sort.FieldName);
        var expression = Expression.Lambda(memberExpression, parameterExpression);
        var expression2 = Expression.Call(typeof(Queryable), orderBy, [typeof(T), memberExpression.Type],
            query.Expression, Expression.Quote(expression));

        return (IOrderedQueryable<T>) query.Provider.CreateQuery<T>(expression2);
    }

    public static IQueryable<T> Paginate<T>(IQueryable<T> query, Pagination pagination)
    {
        return query
            .Skip((pagination.GetPageNumber - 1) * pagination.GetPageSize)
            .Take(pagination.GetPageSize);
    }
}
