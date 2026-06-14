using UnitConversion.Infrastructure.Persistence.Entities;

namespace UnitConversion.Infrastructure.Persistence;

internal static class UserSeed
{
    public static IReadOnlyList<UserRecord> All { get; } =
    [
        new UserRecord
        {
            Id = 1,
            Username = "demo",
            Password = "Demo@123",
        },
    ];
}
