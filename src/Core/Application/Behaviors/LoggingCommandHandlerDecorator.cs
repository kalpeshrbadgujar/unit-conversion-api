using System.Diagnostics;
using Microsoft.Extensions.Logging;
using UnitConversion.Application.Abstractions;

namespace UnitConversion.Application.Behaviors;

/// <summary>
/// Logs command execution with structured properties for observability.
/// </summary>
public sealed class LoggingCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _inner;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand, TResult>> _logger;

    public LoggingCommandHandlerDecorator(
        ICommandHandler<TCommand, TResult> inner,
        ILogger<LoggingCommandHandlerDecorator<TCommand, TResult>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var commandName = typeof(TCommand).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Handling command {CommandName} {@Command}", commandName, command);

        try
        {
            var result = await _inner.Handle(command, cancellationToken);

            _logger.LogInformation(
                "Handled command {CommandName} in {ElapsedMilliseconds}ms",
                commandName,
                stopwatch.ElapsedMilliseconds);

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Command {CommandName} failed after {ElapsedMilliseconds}ms",
                commandName,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}
