using UnitConversion.Common.Constants;

namespace UnitConversion.Domain.Exceptions;

public sealed class UnitNotFoundException : Exception
{
    public UnitNotFoundException(string unitCode)
        : base(ConversionMessages.UnitNotSupported(unitCode))
    {
        UnitCode = unitCode;
    }

    public string UnitCode { get; }
}
