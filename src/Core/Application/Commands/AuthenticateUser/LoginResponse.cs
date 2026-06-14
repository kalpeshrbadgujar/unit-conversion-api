namespace UnitConversion.Application.Commands.AuthenticateUser;

public sealed class LoginResponse
{
    public required string AccessToken { get; init; }

    public string TokenType { get; init; } = "Bearer";

    public int ExpiresInMinutes { get; init; }
}
