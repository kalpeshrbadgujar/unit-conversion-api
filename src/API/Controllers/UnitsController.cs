using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnitConversion.Application.Queries.GetUnits;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Api.Controllers;

[ApiController]
[Route("api/units")]
public sealed class UnitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnitsController(IMediator mediator)
    {
        _mediator = mediator;
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
        var units = await _mediator.Send(new GetUnitsQuery(category), cancellationToken);
        return Ok(units);
    }
}
