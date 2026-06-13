using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;
using UnitConversion.Domain.Registry;

namespace UnitConversion.Infrastructure.Persistence;

/// <summary>
/// In-memory unit catalog with seed data for local testing
/// </summary>
public sealed class InMemoryUnitRepository : IUnitRepository
{
    public Task<IReadOnlyList<UnitDefinition>> GetAllAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(UnitCatalog.SeedUnits);

    public Task<IReadOnlyList<UnitDefinition>> GetByCategoryAsync(
        ConversionCategory category,
        CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<UnitDefinition>>(
            UnitCatalog.SeedUnits.Where(unit => unit.Category == category).ToList());

    public Task<UnitDefinition?> GetByCodeAsync(string unitCode, CancellationToken cancellationToken = default) =>
        Task.FromResult(
            UnitCatalog.SeedUnits.FirstOrDefault(unit =>
                string.Equals(unit.Code, unitCode, StringComparison.OrdinalIgnoreCase)));
}
