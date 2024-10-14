namespace Sekmen.Commerce.Services.CouponApplication.Segregation;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;