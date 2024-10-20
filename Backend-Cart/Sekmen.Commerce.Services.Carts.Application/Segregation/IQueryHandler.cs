namespace Sekmen.Commerce.Services.Carts.Application.Segregation;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;