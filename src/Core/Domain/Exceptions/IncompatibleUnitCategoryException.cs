using UnitConversion.Common.Constants;

namespace UnitConversion.Domain.Exceptions;

public sealed class IncompatibleUnitCategoryException : Exception
{
    public IncompatibleUnitCategoryException(string fromUnit, string toUnit)
        : base(ConversionMessages.IncompatibleCategories(fromUnit, toUnit))
    {
        FromUnit = fromUnit;
        ToUnit = toUnit;
    }

    public string FromUnit { get; }

    public string ToUnit { get; }
}
