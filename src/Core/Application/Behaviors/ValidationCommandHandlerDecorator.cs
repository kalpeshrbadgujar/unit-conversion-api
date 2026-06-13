using FluentValidation;
using UnitConversion.Application.Abstractions;

namespace UnitConversion.Application.Behaviors;

/// <summary>
/// Runs FluentValidation before delegating to the inner command handler.
/// </summary>
public sealed class ValidationCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _inner;
    private readonly IValidator<TCommand> _validator;

    public ValidationCommandHandlerDecorator(
        ICommandHandler<TCommand, TResult> inner,
        IValidator<TCommand> validator)
    {
        _inner = inner;
        _validator = validator;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);
        return await _inner.Handle(command, cancellationToken);
    }
}
