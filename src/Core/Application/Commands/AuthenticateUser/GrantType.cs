namespace UnitConversion.Application.Commands.AuthenticateUser;

/// <summary>
/// OAuth-style grant types. Only <see cref="Password"/> has a registered <see cref="Services.Grants.IGrantAuthService"/> today.
/// </summary>
public enum GrantType
{
    Password,
    RefreshToken,
    ClientCredentials,
}
