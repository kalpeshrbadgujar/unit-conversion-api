using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Resolves category-specific conversion strategies.
/// </summary>
public sealed class ConversionStrategyFactory : IConversionStrategyFactory
{
    private readonly IReadOnlyDictionary<ConversionCategory, IConversionStrategy> _strategies;

    public ConversionStrategyFactory(IEnumerable<IConversionStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(strategy => strategy.Category);
    }

    public IConversionStrategy GetStrategy(ConversionCategory category)
    {
        if (_strategies.TryGetValue(category, out var strategy))
        {
            return strategy;
        }

        throw new NotSupportedException($"No conversion strategy is registered for category '{category}'.");
    }
}
