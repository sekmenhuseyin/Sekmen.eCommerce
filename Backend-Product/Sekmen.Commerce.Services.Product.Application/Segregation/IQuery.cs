namespace Sekmen.Commerce.Services.Products.Application.Segregation;

public interface IQuery<out TResponse> : IRequest<TResponse>;

public interface ICachedQuery<out TResponse> : IQuery<TResponse>
{
    string GenerateCacheKey();
    int CacheDurationMinutes { get; }
}