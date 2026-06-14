using UnitConversion.Common.Enums;

namespace UnitConversion.Application.Services.Grants;

/// <summary>
/// Resolves <see cref="IGrantAuthService"/> by <see cref="GrantType"/> from DI-registered implementations.
/// </summary>
public sealed class GrantAuthServiceProvider : IGrantAuthServiceProvider
{
    private readonly IReadOnlyDictionary<GrantType, IGrantAuthService> _services;

    public GrantAuthServiceProvider(IEnumerable<IGrantAuthService> services)
    {
        _services = services.ToDictionary(service => service.GrantType);
    }

    public IGrantAuthService? GetService(GrantType grantType) =>
        _services.GetValueOrDefault(grantType);
}
