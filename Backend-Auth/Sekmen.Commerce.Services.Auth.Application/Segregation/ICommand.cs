namespace Sekmen.Commerce.Services.Auth.Application.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
