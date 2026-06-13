using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UnitConversion.Application.Abstractions;
using UnitConversion.Application.Behaviors;
using UnitConversion.Application.Commands.ConvertUnit;
using UnitConversion.Application.Queries.GetUnits;
using UnitConversion.Domain.Models;

namespace UnitConversion.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddLoggedQueryHandler<GetUnitsQuery, IReadOnlyList<UnitDefinition>, GetUnitsQueryHandler>();
        services.AddLoggedValidatedCommandHandler<ConvertUnitCommand, ConversionResult, ConvertUnitCommandHandler>();

        return services;
    }

    private static IServiceCollection AddLoggedQueryHandler<TQuery, TResult, THandler>(
        this IServiceCollection services)
        where THandler : class, IQueryHandler<TQuery, TResult>
    {
        services.AddScoped<THandler>();
        services.AddScoped<IQueryHandler<TQuery, TResult>>(serviceProvider =>
            new LoggingQueryHandlerDecorator<TQuery, TResult>(
                serviceProvider.GetRequiredService<THandler>(),
                serviceProvider.GetRequiredService<ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>>>()));

        return services;
    }

    private static IServiceCollection AddLoggedValidatedCommandHandler<TCommand, TResult, THandler>(
        this IServiceCollection services)
        where THandler : class, ICommandHandler<TCommand, TResult>
    {
        services.AddScoped<THandler>();
        services.AddScoped<ICommandHandler<TCommand, TResult>>(serviceProvider =>
        {
            var inner = new ValidationCommandHandlerDecorator<TCommand, TResult>(
                serviceProvider.GetRequiredService<THandler>(),
                serviceProvider.GetRequiredService<IValidator<TCommand>>());

            return new LoggingCommandHandlerDecorator<TCommand, TResult>(
                inner,
                serviceProvider.GetRequiredService<ILogger<LoggingCommandHandlerDecorator<TCommand, TResult>>>());
        });

        return services;
    }
}
