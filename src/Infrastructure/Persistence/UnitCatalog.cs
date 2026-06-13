using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Infrastructure.Persistence;

/// <summary>
/// Seed catalog used by the in-memory repository and API documentation.
/// </summary>
public static class UnitCatalog
{
    public static IReadOnlyList<UnitDefinition> SeedUnits { get; } =
    [
        new() { Code = "meter", Name = "Meter", Category = ConversionCategory.Length },
        new() { Code = "kilometer", Name = "Kilometer", Category = ConversionCategory.Length },
        new() { Code = "foot", Name = "Foot", Category = ConversionCategory.Length },
        new() { Code = "kilogram", Name = "Kilogram", Category = ConversionCategory.Weight },
        new() { Code = "pound", Name = "Pound", Category = ConversionCategory.Weight },
        new() { Code = "celsius", Name = "Celsius", Category = ConversionCategory.Temperature },
        new() { Code = "fahrenheit", Name = "Fahrenheit", Category = ConversionCategory.Temperature },
        new() { Code = "liter", Name = "Liter", Category = ConversionCategory.Volume },
        new() { Code = "gallon", Name = "Gallon", Category = ConversionCategory.Volume },
    ];
}
