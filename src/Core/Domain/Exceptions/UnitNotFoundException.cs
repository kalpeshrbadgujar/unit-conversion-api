namespace UnitConversion.Domain.Exceptions;

public sealed class UnitNotFoundException : Exception
{
    public UnitNotFoundException(string unitCode)
        : base($"Unit '{unitCode}' is not supported.")
    {
        UnitCode = unitCode;
    }

    public string UnitCode { get; }
}
