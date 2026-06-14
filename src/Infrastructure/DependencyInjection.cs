using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitConversion.Domain.Auth;
using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Registry;
using UnitConversion.Infrastructure.Authentication;
using UnitConversion.Infrastructure.Conversion;
using UnitConversion.Infrastructure.Persistence;

namespace UnitConversion.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddSingleton<IAccessTokenGenerator, JwtAccessTokenGenerator>();

        services.AddSingleton<InMemoryDataContext>();
        services.AddSingleton<IUnitRepository, InMemoryUnitRepository>();
        services.AddSingleton<IUserRepository>(serviceProvider =>
            new InMemoryUserRepository(serviceProvider.GetRequiredService<InMemoryDataContext>()));

        services.AddSingleton<IConversionStrategy, LengthConversionStrategy>();
        services.AddSingleton<IConversionStrategy, WeightConversionStrategy>();
        services.AddSingleton<IConversionStrategy, TemperatureConversionStrategy>();
        services.AddSingleton<IConversionStrategy, VolumeConversionStrategy>();
        services.AddSingleton<IConversionStrategyFactory, ConversionStrategyFactory>();

        return services;
    }
}
