using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Exceptions;
using UnitConversion.Domain.Models;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Converts units within a category using factors relative to a base unit.
/// </summary>
public abstract class LinearConversionStrategyBase : IConversionStrategy
{
    public abstract ConversionCategory Category { get; }

    protected abstract IReadOnlyDictionary<string, decimal> FactorsToBaseUnit { get; }

    public ConversionResult Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit)
    {
        if (fromUnit.Category != Category || toUnit.Category != Category)
        {
            throw new IncompatibleUnitCategoryException(fromUnit.Code, toUnit.Code);
        }

        if (!FactorsToBaseUnit.TryGetValue(fromUnit.Code, out var fromFactor))
        {
            throw new UnitNotFoundException(fromUnit.Code);
        }

        if (!FactorsToBaseUnit.TryGetValue(toUnit.Code, out var toFactor))
        {
            throw new UnitNotFoundException(toUnit.Code);
        }

        var baseValue = value * fromFactor;
        var resultValue = baseValue / toFactor;

        return new ConversionResult
        {
            InputValue = value,
            FromUnit = fromUnit.Code,
            ToUnit = toUnit.Code,
            ResultValue = resultValue,
            Category = Category,
        };
    }
}
