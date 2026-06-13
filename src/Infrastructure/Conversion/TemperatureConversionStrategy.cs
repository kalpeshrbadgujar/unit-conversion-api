using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Exceptions;
using UnitConversion.Domain.Models;

namespace UnitConversion.Infrastructure.Conversion;

/// <summary>
/// Temperature conversions between Celsius and Fahrenheit.
/// </summary>
public sealed class TemperatureConversionStrategy : IConversionStrategy
{
    public ConversionCategory Category => ConversionCategory.Temperature;

    public ConversionResult Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit)
    {
        if (fromUnit.Category != Category || toUnit.Category != Category)
        {
            throw new IncompatibleUnitCategoryException(fromUnit.Code, toUnit.Code);
        }

        var celsius = ToCelsius(value, fromUnit.Code);
        var resultValue = FromCelsius(celsius, toUnit.Code);

        return new ConversionResult
        {
            InputValue = value,
            FromUnit = fromUnit.Code,
            ToUnit = toUnit.Code,
            ResultValue = resultValue,
            Category = Category,
        };
    }

    private static decimal ToCelsius(decimal value, string unitCode) =>
        unitCode.ToLowerInvariant() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32m) * 5m / 9m,
            _ => throw new UnitNotFoundException(unitCode),
        };

    private static decimal FromCelsius(decimal celsius, string unitCode) =>
        unitCode.ToLowerInvariant() switch
        {
            "celsius" => celsius,
            "fahrenheit" => celsius * 9m / 5m + 32m,
            _ => throw new UnitNotFoundException(unitCode),
        };
}
