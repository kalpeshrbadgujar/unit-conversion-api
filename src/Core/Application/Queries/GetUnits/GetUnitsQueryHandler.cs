using UnitConversion.Application.Abstractions;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;
using UnitConversion.Domain.Registry;

namespace UnitConversion.Application.Queries.GetUnits;

public sealed class GetUnitsQueryHandler : IQueryHandler<GetUnitsQuery, IReadOnlyList<UnitDefinition>>
{
    private readonly IUnitRepository _unitRepository;

    public GetUnitsQueryHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<IReadOnlyList<UnitDefinition>> Handle(GetUnitsQuery query, CancellationToken cancellationToken)
    {
        if (query.Category is ConversionCategory category)
        {
            return await _unitRepository.GetByCategoryAsync(category, cancellationToken);
        }

        return await _unitRepository.GetAllAsync(cancellationToken);
    }
}
