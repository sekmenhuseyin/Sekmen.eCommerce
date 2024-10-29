using Sekmen.Commerce.Carts.Application.Models;

namespace Sekmen.Commerce.Carts.Application.Segregation;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse: Result;
