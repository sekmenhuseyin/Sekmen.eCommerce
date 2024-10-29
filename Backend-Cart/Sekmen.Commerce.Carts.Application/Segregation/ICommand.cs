using Sekmen.Commerce.Carts.Application.Models;

namespace Sekmen.Commerce.Carts.Application.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
