using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Volume conversions with liter as the base unit (US liquid gallon).
/// </summary>
public sealed class VolumeConversionStrategy : LinearConversionStrategyBase
{
    public override ConversionCategory Category => ConversionCategory.Volume;

    protected override IReadOnlyDictionary<string, decimal> FactorsToBaseUnit { get; } =
        new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            ["liter"] = 1m,
            ["gallon"] = 3.785411784m,
        };
}
