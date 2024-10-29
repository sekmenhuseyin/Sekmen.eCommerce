using Sekmen.Commerce.Auth.Shared.Models;

namespace Sekmen.Commerce.Auth.Shared.Segregation;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse: Result;
