namespace Sekmen.Commerce.Services.CouponApplication.Segregation;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>;
