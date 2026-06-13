using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Weight conversions with kilogram as the base unit.
/// </summary>
public sealed class WeightConversionStrategy : LinearConversionStrategyBase
{
    public override ConversionCategory Category => ConversionCategory.Weight;

    protected override IReadOnlyDictionary<string, decimal> FactorsToBaseUnit { get; } =
        new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            ["kilogram"] = 1m,
            ["pound"] = 0.45359237m,
            ["tonne"] = 1000m,
            ["gram"] = 0.001m,
            ["ounce"] = 0.028349523125m,
        };
}
