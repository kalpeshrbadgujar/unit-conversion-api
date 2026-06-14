using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Models;

/// <summary>
/// Describes a supported unit of measurement in the registry.
/// </summary>
public sealed record UnitDefinition
{
    /// <summary>
    /// Stable identifier used in API requests (e.g. "meter", "square_meter", "acre").
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Unit name
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Conversion category
    /// </summary>
    public required ConversionCategory Category { get; init; }
}
