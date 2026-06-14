using UnitConversion.Infrastructure.Persistence.Entities;

namespace UnitConversion.Infrastructure.Persistence;

/// <summary>
/// In-memory stand-in for a database context. Seed data lives here until an EF Core context replaces it.
/// </summary>
internal sealed class InMemoryDataContext
{
    public IReadOnlyList<UserRecord> Users { get; }

    public InMemoryDataContext()
    {
        Users = UserSeed.All;
    }
}
