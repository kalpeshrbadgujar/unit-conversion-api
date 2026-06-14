using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;
using UnitConversion.Infrastructure.Conversion;

namespace UnitConversion.Tests.Conversion;

public sealed class WeightConversionStrategyTests
{
    private readonly WeightConversionStrategy _strategy = new();

    [Theory]
    [InlineData(1000, "gram", "kilogram", 1)]
    [InlineData(1, "kilogram", "gram", 1000)]
    [InlineData(16, "ounce", "pound", 1)]
    [InlineData(1, "tonne", "kilogram", 1000)]
    public void Convert_ReturnsExpectedValue(
        decimal input,
        string fromUnit,
        string toUnit,
        decimal expected)
    {
        var result = _strategy.Convert(
            input,
            CreateUnit(fromUnit, ConversionCategory.Weight),
            CreateUnit(toUnit, ConversionCategory.Weight));

        Assert.Equal(expected, result.ResultValue);
    }

    private static UnitDefinition CreateUnit(string code, ConversionCategory category) =>
        new()
        {
            Code = code,
            Name = code,
            Category = category,
        };
}
