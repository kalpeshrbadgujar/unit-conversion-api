using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnitConversion.Application.Abstractions;
using UnitConversion.Application.Commands.ConvertUnit;
using UnitConversion.Application.Queries.GetUnits;
using UnitConversion.Domain.Models;

namespace UnitConversion.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IQueryHandler<GetUnitsQuery, IReadOnlyList<UnitDefinition>>, GetUnitsQueryHandler>();
        services.AddScoped<ICommandHandler<ConvertUnitCommand, ConversionResult>, ConvertUnitCommandHandler>();

        return services;
    }
}
