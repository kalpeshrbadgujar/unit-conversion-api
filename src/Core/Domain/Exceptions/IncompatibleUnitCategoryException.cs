namespace UnitConversion.Domain.Exceptions;

public sealed class IncompatibleUnitCategoryException : Exception
{
    public IncompatibleUnitCategoryException(string fromUnit, string toUnit)
        : base($"Cannot convert between '{fromUnit}' and '{toUnit}' because they belong to different categories.")
    {
        FromUnit = fromUnit;
        ToUnit = toUnit;
    }

    public string FromUnit { get; }

    public string ToUnit { get; }
}
