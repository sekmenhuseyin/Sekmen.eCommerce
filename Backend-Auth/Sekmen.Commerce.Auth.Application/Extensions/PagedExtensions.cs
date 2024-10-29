// ReSharper disable MemberCanBePrivate.Global
namespace Sekmen.Commerce.Auth.Application.Extensions;

public static class PagedExtensions
{
    public static IQueryable<TAny> GetPaged<TAny>(this IQueryable<TAny> query, IPagedFilter paged)
        where TAny : class
    {
        return GetPaged(query, paged.PageIndex, paged.PageSize);
    }

    public static IQueryable<TAny> GetPaged<TAny>(this IQueryable<TAny> query, int pageIndex, int pageSize) where TAny : class
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    public static IEnumerable<TAny> GetPaged<TAny>(this IEnumerable<TAny> query, IPagedFilter paged)
        where TAny : class
    {
        return GetPaged(query, paged.PageIndex, paged.PageSize);
    }

    public static IEnumerable<TAny> GetPaged<TAny>(this IEnumerable<TAny> query, int pageIndex, int pageSize) where TAny : class
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    public static IPagedQueryResult<IEnumerable<TAny>> ToPagedQueryResult<TAny>(this IEnumerable<TAny> obj, IPagedFilter filter, int rowCount)
    {
        return obj.ToPagedQueryResult(filter.PageIndex, filter.PageSize, rowCount);
    }

    public static IPagedQueryResult<IEnumerable<TAny>> ToPagedQueryResult<TAny>(this IEnumerable<TAny> obj, int pageIndex, int pageSize, int rowCount)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        return new PagedQueryResult<IEnumerable<TAny>>(obj, pageIndex, rowCount, pageSize);
    }
}