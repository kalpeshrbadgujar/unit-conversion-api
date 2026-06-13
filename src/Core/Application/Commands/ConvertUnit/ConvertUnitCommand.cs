using MediatR;
using UnitConversion.Domain.Models;

namespace UnitConversion.Application.Commands.ConvertUnit;

public sealed record ConvertUnitCommand(decimal Value, string FromUnit, string ToUnit)
                                        : IRequest<ConversionResult>;
