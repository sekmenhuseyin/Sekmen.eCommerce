namespace Sekmen.Commerce.Services.Coupons.Application.Segregation;

public interface IPageQuery<out TResponse> : IPagedFilter, IQuery<TResponse>;

public interface IFilter;

public interface IPagedFilter : IFilter
{
    int PageIndex { get; set; }
    int PageSize { get; set; }
    string OrderBy { get; set; }
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
    
public abstract class FilterBase : IPagedFilter
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; } = string.Empty;
}