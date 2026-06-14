using UnitConversion.Common.Enums;

namespace UnitConversion.Common.Constants;

/// <summary>
/// User-facing authentication messages shared across Application and API layers.
/// </summary>
public static class AuthMessages
{
    public const string InvalidCredentials = "Invalid username or password.";

    public const string UnsupportedGrantType = "Unsupported grant type.";

    public static string GrantNotSupportedYet(GrantType grantType) =>
        $"{grantType} grant is not supported yet.";
}
