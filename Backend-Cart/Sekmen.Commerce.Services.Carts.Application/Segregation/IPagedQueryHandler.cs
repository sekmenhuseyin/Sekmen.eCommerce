namespace Sekmen.Commerce.Services.Carts.Application.Segregation;

public interface IPagedQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IPagedQuery<TResponse>
    where TResponse: IPagedQueryResult<IEnumerable>;