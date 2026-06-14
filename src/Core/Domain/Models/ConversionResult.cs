using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Models;

/// <summary>
/// Response model for conversion result
/// </summary>
public sealed record ConversionResult
{
    /// <summary>
    /// Value to convert
    /// </summary>
    public required decimal InputValue { get; init; }

    /// <summary>
    /// Source unit code (e.g. "meter").
    /// </summary>
    public required string FromUnit { get; init; }

    /// <summary>
    /// Target unit code (e.g. "foot").
    /// </summary>
    public required string ToUnit { get; init; }

    /// <summary>
    /// Converted value
    /// </summary>
    public required decimal ResultValue { get; init; }

    /// <summary>
    /// Category shared by the source and target units.
    /// </summary>
    public required ConversionCategory Category { get; init; }
}
