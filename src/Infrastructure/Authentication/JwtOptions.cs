using UnitConversion.Common.Constants;

namespace UnitConversion.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = ConfigurationSections.AuthenticationJwt;

    public string Issuer { get; init; } = "UnitConversion.Api";

    public string Audience { get; init; } = "UnitConversion.Api";

    public string SigningKey { get; init; } = string.Empty;

    public int ExpirationMinutes { get; init; } = 60;
}
