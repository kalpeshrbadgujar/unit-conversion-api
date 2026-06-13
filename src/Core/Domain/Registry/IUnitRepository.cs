using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Domain.Registry;

/// <summary>
/// Read-only catalog of supported units. Implementations is in in-memory as of now
/// Can be extended to support DB operations
/// </summary>
public interface IUnitRepository
{
    Task<IReadOnlyList<UnitDefinition>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UnitDefinition>> GetByCategoryAsync(
        ConversionCategory category,
        CancellationToken cancellationToken = default);

    Task<UnitDefinition?> GetByCodeAsync(string unitCode, CancellationToken cancellationToken = default);
}
