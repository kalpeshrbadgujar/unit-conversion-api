namespace UnitConversion.Api.Contracts;

/// <summary>
/// Request body for unit conversion.
/// </summary>
public sealed record ConvertUnitRequest
{
    /// <summary>
    /// Numeric amount to convert.
    /// </summary>
    /// <example>100</example>
    public required decimal Value { get; init; }

    /// <summary>
    /// Source unit code (e.g. meter). See GET /api/units for all supported codes.
    /// </summary>
    /// <example>meter</example>
    public required string FromUnit { get; init; }

    /// <summary>
    /// Target unit code (e.g. kilometer). Must be in the same category as <see cref="FromUnit"/>.
    /// </summary>
    /// <example>kilometer</example>
    public required string ToUnit { get; init; }
}
