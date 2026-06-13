using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnitConversion.Api.Contracts;
using UnitConversion.Application.Commands.ConvertUnit;
using UnitConversion.Domain.Models;

namespace UnitConversion.Api.Controllers;

[ApiController]
[Route("api/convert")]
public sealed class ConversionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConversionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Converts a value between two units in the same category.
    /// Call GET /api/units first to see valid unit codes (e.g. meter, kilometer, celsius).
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ConversionResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<ConversionResult>> Convert(
        [FromBody] ConvertUnitRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ConvertUnitCommand(request.Value, request.FromUnit, request.ToUnit),
            cancellationToken);

        return Ok(result);
    }
}
