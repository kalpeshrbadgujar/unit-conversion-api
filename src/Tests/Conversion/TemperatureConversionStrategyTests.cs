using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;
using UnitConversion.Infrastructure.Conversion;

namespace UnitConversion.Tests.Conversion;

public sealed class TemperatureConversionStrategyTests
{
    private readonly TemperatureConversionStrategy _strategy = new();

    [Theory]
    [InlineData(0, "celsius", "fahrenheit", 32)]
    [InlineData(32, "fahrenheit", "celsius", 0)]
    [InlineData(100, "celsius", "fahrenheit", 212)]
    public void Convert_ReturnsExpectedValue(
        decimal input,
        string fromUnit,
        string toUnit,
        decimal expected)
    {
        var result = _strategy.Convert(
            input,
            CreateUnit(fromUnit),
            CreateUnit(toUnit));

        Assert.Equal(expected, result.ResultValue);
    }

    private static UnitDefinition CreateUnit(string code) =>
        new()
        {
            Code = code,
            Name = code,
            Category = ConversionCategory.Temperature,
        };
}
