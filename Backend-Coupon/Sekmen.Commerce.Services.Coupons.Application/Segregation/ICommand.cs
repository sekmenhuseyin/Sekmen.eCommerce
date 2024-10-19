namespace Sekmen.Commerce.Services.Coupons.Application.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
