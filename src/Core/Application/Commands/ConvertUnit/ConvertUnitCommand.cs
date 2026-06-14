namespace UnitConversion.Application.Commands.ConvertUnit;

public sealed record ConvertUnitCommand(decimal Value, string FromUnit, string ToUnit);
