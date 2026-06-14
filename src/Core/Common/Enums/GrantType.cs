namespace UnitConversion.Common.Enums;

/// <summary>
/// OAuth-style grant types for token issuance.
/// Only <see cref="Password"/> is implemented; register additional grant services in DI to extend.
/// </summary>
public enum GrantType
{
    Password,
    RefreshToken,
    ClientCredentials,
}
