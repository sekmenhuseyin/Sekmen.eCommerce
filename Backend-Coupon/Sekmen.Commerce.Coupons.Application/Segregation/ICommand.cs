using Sekmen.Commerce.Coupons.Application.Models;

namespace Sekmen.Commerce.Coupons.Application.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse: Result;
