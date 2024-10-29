using Sekmen.Commerce.Products.Application.Models;

namespace Sekmen.Commerce.Products.Application.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
