using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Length conversions with meter as the base unit.
/// </summary>
public sealed class LengthConversionStrategy : LinearConversionStrategyBase
{
    public override ConversionCategory Category => ConversionCategory.Length;

    protected override IReadOnlyDictionary<string, decimal> FactorsToBaseUnit { get; } =
        new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            ["meter"] = 1m,
            ["kilometer"] = 1000m,
            ["foot"] = 0.3048m,
        };
}
