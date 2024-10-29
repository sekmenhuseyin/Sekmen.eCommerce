namespace Sekmen.Commerce.Auth.Shared.Segregation;

public interface IPagedQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IPagedQuery<TResponse>
    where TResponse: IPagedQueryResult<IEnumerable>;