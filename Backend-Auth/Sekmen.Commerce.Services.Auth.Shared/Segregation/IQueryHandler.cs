namespace Sekmen.Commerce.Services.Auth.Shared.Segregation;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;