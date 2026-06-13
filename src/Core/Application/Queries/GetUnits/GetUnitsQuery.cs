using MediatR;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Application.Queries.GetUnits;

public sealed record GetUnitsQuery(ConversionCategory? Category = null)
                                : IRequest<IReadOnlyList<UnitDefinition>>;
