namespace UnitConversion.Infrastructure.Persistence.Entities;

internal sealed class UserRecord
{
    public int Id { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }
}
