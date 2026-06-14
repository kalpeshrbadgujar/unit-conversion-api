using UnitConversion.Application.Abstractions;
using UnitConversion.Domain.Conversion;
using UnitConversion.Domain.Exceptions;
using UnitConversion.Domain.Models;
using UnitConversion.Domain.Registry;

namespace UnitConversion.Application.Commands.ConvertUnit;

public sealed class ConvertUnitCommandHandler : ICommandHandler<ConvertUnitCommand, ConversionResult>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IConversionStrategyFactory _strategyFactory;

    public ConvertUnitCommandHandler(
        IUnitRepository unitRepository,
        IConversionStrategyFactory strategyFactory)
    {
        _unitRepository = unitRepository;
        _strategyFactory = strategyFactory;
    }

    public async Task<ConversionResult> Handle(ConvertUnitCommand command, CancellationToken cancellationToken)
    {
        var fromUnit = await _unitRepository.GetByCodeAsync(command.FromUnit, cancellationToken)
            ?? throw new UnitNotFoundException(command.FromUnit);

        var toUnit = await _unitRepository.GetByCodeAsync(command.ToUnit, cancellationToken)
            ?? throw new UnitNotFoundException(command.ToUnit);

        if (fromUnit.Category != toUnit.Category)
        {
            throw new IncompatibleUnitCategoryException(fromUnit.Code, toUnit.Code);
        }

        var strategy = _strategyFactory.GetStrategy(fromUnit.Category);
        return strategy.Convert(command.Value, fromUnit, toUnit);
    }
}
