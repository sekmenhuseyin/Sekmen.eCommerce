using Sekmen.Commerce.Products.Application.Models;

namespace Sekmen.Commerce.Products.Application.Segregation;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse: Result;
