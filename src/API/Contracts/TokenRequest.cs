using UnitConversion.Application.Commands.AuthenticateUser;

namespace UnitConversion.Api.Contracts;

public sealed class TokenRequest
{
    public GrantType GrantType { get; init; } = GrantType.Password;

    public string? Username { get; init; }

    public string? Password { get; init; }
}
