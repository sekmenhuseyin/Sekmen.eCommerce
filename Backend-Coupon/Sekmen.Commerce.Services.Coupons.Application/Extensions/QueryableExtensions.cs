// ReSharper disable MemberCanBePrivate.Global
namespace Sekmen.Commerce.Services.Coupons.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> FilterIf<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, bool runCondition) where TEntity : class
    {
        return runCondition ? query.Where(predicate) : query;
    }

    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, object value) where TEntity : class
    {
        if ((value as bool?).HasValue)
            return query.Where(predicate);

        return value switch
        {
            string when !string.IsNullOrWhiteSpace(value.ToString()) => query.Where(predicate),
            int when Convert.ToInt32(value) > 0 => query.Where(predicate),
            decimal when Convert.ToDecimal(value) > 0 => query.Where(predicate),
            double when Convert.ToDouble(value) > 0 => query.Where(predicate),
            float when Convert.ToDouble(value) > 0 => query.Where(predicate),
            short when Convert.ToSByte(value) > 0 => query.Where(predicate),
            int[] list when list.Any() => query.Where(predicate),
            Enum when Convert.ToInt32(value) > 0 => query.Where(predicate),
            DateTime when Convert.ToDateTime(value) > DateTime.MinValue => query.Where(predicate),
            List<int> list when list.Any() => query.Where(predicate),
            List<string> obj when obj.Any() => query.Where(predicate),
            string[] arr when arr.Any() => query.Where(predicate),
            _ => query
        };
    }

    public static IQueryable<TEntity> Sort<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, TKey>> predicate, string[] values) where TEntity : class
    {
        values = values.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        foreach (var val in values)
        {
            var split = val.ToLowerInvariant().Split('_');
            var direction = split.Length > 1 ? split[1] : string.Empty;
            if (split[0] == GetMemberExpressions(predicate.Body).FirstOrDefault()?.Member.Name.ToLowerInvariant().Replace("ı", "i"))
                return direction is "desc" or "descend"
                    ? query.OrderByDescending(predicate)
                    : query.OrderBy(predicate);
        }

        return query;
    }

    public static IQueryable<TEntity> Sort<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, TKey>> predicate, string value) where TEntity : class
    {
        return Sort(query, predicate, [value]);
    }

    private static IEnumerable<MemberExpression> GetMemberExpressions(Expression body)
    {
        var candidates = new Queue<Expression>([body]);
        while (candidates.Count > 0)
        {
            var expr = candidates.Dequeue();
            switch (expr)
            {
                case MemberExpression memberExpression:
                    yield return memberExpression;
                    break;

                case UnaryExpression unaryExpression:
                    candidates.Enqueue(unaryExpression.Operand);
                    break;

                case BinaryExpression binaryExpression:
                    candidates.Enqueue(binaryExpression.Left);
                    candidates.Enqueue(binaryExpression.Right);
                    break;

                case MethodCallExpression methodCallExpression:
                    foreach (var argument in methodCallExpression.Arguments)
                        candidates.Enqueue(argument);
                    break;

                case LambdaExpression lambdaExpression:
                    candidates.Enqueue(lambdaExpression.Body);
                    break;
            }
        }
    }
}