using Sekmen.Commerce.Auth.Shared.Models;

namespace Sekmen.Commerce.Auth.Shared.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
