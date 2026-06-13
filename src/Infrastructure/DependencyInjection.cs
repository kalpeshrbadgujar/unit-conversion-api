using Microsoft.Extensions.DependencyInjection;
using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Registry;
using UnitConversion.Infrastructure.Conversion;
using UnitConversion.Infrastructure.Persistence;

namespace UnitConversion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUnitRepository, InMemoryUnitRepository>();
        services.AddSingleton<IConversionStrategyFactory, ConversionStrategyFactory>();

        return services;
    }
}
