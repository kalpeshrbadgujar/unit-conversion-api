using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnitConversion.Api.Contracts;
using UnitConversion.Application.Abstractions;
using UnitConversion.Application.Commands.AuthenticateUser;

namespace UnitConversion.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ICommandHandler<AuthenticateUserCommand, AuthenticateUserResult> _handler;

    public AuthController(ICommandHandler<AuthenticateUserCommand, AuthenticateUserResult> handler)
    {
        _handler = handler;
    }

    /// <summary>
    /// Issues a JWT when credentials are valid — only GrantType => Password is implemented; others return 501.
    /// </summary>
    [HttpPost("token")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] TokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _handler.Handle(
            new AuthenticateUserCommand(request.GrantType, request.Username, request.Password),
            cancellationToken);

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result.Response);
        }

        return StatusCode((int)result.StatusCode, new { message = result.Message });
    }
}
