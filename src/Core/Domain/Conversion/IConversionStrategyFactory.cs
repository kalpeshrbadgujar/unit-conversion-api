using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Conversion;

/// <summary>
/// Resolves the conversion strategy for a category (length, weight, temperature, etc.).
/// </summary>
public interface IConversionStrategyFactory
{
    IConversionStrategy GetStrategy(ConversionCategory category);
}
