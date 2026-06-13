using MediatR;
using UnitConversion.Domain.Models;

namespace UnitConversion.Application.Commands.ConvertUnit;

public sealed class ConvertUnitCommandHandler : IRequestHandler<ConvertUnitCommand, ConversionResult>
{
    public Task<ConversionResult> Handle(ConvertUnitCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Conversion logic will be added in a later commit.");
    }
}
