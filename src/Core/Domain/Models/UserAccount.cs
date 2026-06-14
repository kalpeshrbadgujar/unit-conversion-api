namespace UnitConversion.Domain.Models;

/// <summary>
/// Authenticated user identity exposed to the application layer.
/// </summary>
public sealed record UserAccount
{
    public required int Id { get; init; }

    public required string Username { get; init; }
}
