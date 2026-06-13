using Microsoft.AspNetCore.Mvc;
using UnitConversion.Application.Abstractions;
using UnitConversion.Application.Queries.GetUnits;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Api.Controllers;

[ApiController]
[Route("api/units")]
public sealed class UnitsController : ControllerBase
{
    private readonly IQueryHandler<GetUnitsQuery, IReadOnlyList<UnitDefinition>> _handler;

    public UnitsController(IQueryHandler<GetUnitsQuery, IReadOnlyList<UnitDefinition>> handler)
    {
        _handler = handler;
    }

    /// <summary>
    /// Returns supported units, optionally filtered by category.
    /// </summary>
    /// <param name="category">Optional. One of: Length, Weight, Temperature, Volume.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<UnitDefinition>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<UnitDefinition>>> GetUnits(
        [FromQuery] ConversionCategory? category,
        CancellationToken cancellationToken)
    {
        var units = await _handler.Handle(new GetUnitsQuery(category), cancellationToken);
        return Ok(units);
    }
}
