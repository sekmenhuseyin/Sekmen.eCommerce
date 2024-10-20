namespace Sekmen.Commerce.Services.Carts.Application.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
