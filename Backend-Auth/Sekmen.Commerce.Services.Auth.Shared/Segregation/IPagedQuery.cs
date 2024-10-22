namespace Sekmen.Commerce.Services.Auth.Shared.Segregation;

public interface IPagedQuery<out TResponse> : IPagedFilter, IQuery<TResponse>;

public interface IFilter;

public interface IPagedFilter : IFilter
{
    int PageIndex { get; init; }
    int PageSize { get; init; }
    string OrderBy { get; init; }
}

public interface IPagedQuery
{
    int PageIndex { get; }
    int PageSize { get; }
    int RowCount { get; }
}

public interface IPagedQueryResult<out TResult> : IPagedQuery where TResult : IEnumerable
{
    TResult Result { get; }
}

public record PagedQueryResult<TResult>(TResult Result, int PageIndex, int RowCount, int PageSize)
    : IPagedQueryResult<TResult>
    where TResult : IEnumerable;