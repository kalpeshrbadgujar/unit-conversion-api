using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Resolves category-specific conversion strategies. 
public sealed class ConversionStrategyFactory : IConversionStrategyFactory
{
    public IConversionStrategy GetStrategy(ConversionCategory category) =>
        throw new NotImplementedException("Conversion strategies will be registered in a later commit.");
}
