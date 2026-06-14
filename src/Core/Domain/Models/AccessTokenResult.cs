namespace UnitConversion.Domain.Models;

public sealed record AccessTokenResult
{
    public required string AccessToken { get; init; }

    public required int ExpiresInMinutes { get; init; }
}
