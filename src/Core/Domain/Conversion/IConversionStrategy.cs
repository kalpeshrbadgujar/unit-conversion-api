using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Domain.Conversion;

/// <summary>
/// Converts values between units 
/// </summary>
public interface IConversionStrategy
{
    ConversionCategory Category { get; }

    ConversionResult Convert(decimal value, UnitDefinition fromUnit, UnitDefinition toUnit);
}
