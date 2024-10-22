namespace Sekmen.Commerce.Services.Auth.Shared.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
