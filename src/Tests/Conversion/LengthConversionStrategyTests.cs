using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;
using UnitConversion.Infrastructure.Conversion;

namespace UnitConversion.Tests.Conversion;

public sealed class LengthConversionStrategyTests
{
    private readonly LengthConversionStrategy _strategy = new();

    [Theory]
    [InlineData(1000, "meter", "kilometer", 1)]
    [InlineData(1, "kilometer", "meter", 1000)]
    [InlineData(1, "foot", "meter", 0.3048)]
    public void Convert_ReturnsExpectedValue(
        decimal input,
        string fromUnit,
        string toUnit,
        decimal expected)
    {
        var result = _strategy.Convert(
            input,
            CreateUnit(fromUnit, ConversionCategory.Length),
            CreateUnit(toUnit, ConversionCategory.Length));

        Assert.Equal(expected, result.ResultValue);
        Assert.Equal(fromUnit, result.FromUnit);
        Assert.Equal(toUnit, result.ToUnit);
    }

    private static UnitDefinition CreateUnit(string code, ConversionCategory category) =>
        new()
        {
            Code = code,
            Name = code,
            Category = category,
        };
}
