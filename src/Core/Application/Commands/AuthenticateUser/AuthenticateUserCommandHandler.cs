using UnitConversion.Application.Abstractions;
using UnitConversion.Application.Services;

namespace UnitConversion.Application.Commands.AuthenticateUser;

public sealed class AuthenticateUserCommandHandler
    : ICommandHandler<AuthenticateUserCommand, AuthenticateUserResult>
{
    private readonly IAuthService _authService;

    public AuthenticateUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<AuthenticateUserResult> Handle(
        AuthenticateUserCommand command,
        CancellationToken cancellationToken) =>
        _authService.AuthenticateAsync(command, cancellationToken);
}
