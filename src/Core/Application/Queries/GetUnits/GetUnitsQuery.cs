using UnitConversion.Domain.Enums;

namespace UnitConversion.Application.Queries.GetUnits;

public sealed record GetUnitsQuery(ConversionCategory? Category = null);
