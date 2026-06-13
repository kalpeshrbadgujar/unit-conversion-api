using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddQueryHandler<GetUnitsQuery, IReadOnlyList<UnitDefinition>, GetUnitsQueryHandler>();
        services.AddValidatedCommandHandler<ConvertUnitCommand, ConversionResult, ConvertUnitCommandHandler>();

        return services;
    }

    private static IServiceCollection AddQueryHandler<TQuery, TResult, THandler>(
        this IServiceCollection services)
        where THandler : class, IQueryHandler<TQuery, TResult>
    {
        services.AddScoped<IQueryHandler<TQuery, TResult>, THandler>();
        return services;
    }

    private static IServiceCollection AddValidatedCommandHandler<TCommand, TResult, THandler>(
        this IServiceCollection services)
        where THandler : class, ICommandHandler<TCommand, TResult>
    {
        services.AddScoped<THandler>();
        services.AddScoped<ICommandHandler<TCommand, TResult>>(serviceProvider =>
            new ValidationCommandHandlerDecorator<TCommand, TResult>(
                serviceProvider.GetRequiredService<THandler>(),
                serviceProvider.GetRequiredService<IValidator<TCommand>>()));

        return services;
    }
}
