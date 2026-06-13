using System.Diagnostics;
using Microsoft.Extensions.Logging;
using UnitConversion.Application.Abstractions;

namespace UnitConversion.Application.Behaviors;

/// <summary>
/// Logs query execution with structured properties for observability.
/// </summary>
public sealed class LoggingQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _inner;
    private readonly ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> _logger;

    public LoggingQueryHandlerDecorator(
        IQueryHandler<TQuery, TResult> inner,
        ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var queryName = typeof(TQuery).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Handling query {QueryName} {@Query}", queryName, query);

        try
        {
            var result = await _inner.Handle(query, cancellationToken);

            _logger.LogInformation(
                "Handled query {QueryName} in {ElapsedMilliseconds}ms",
                queryName,
                stopwatch.ElapsedMilliseconds);

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Query {QueryName} failed after {ElapsedMilliseconds}ms",
                queryName,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}
