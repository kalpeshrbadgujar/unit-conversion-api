using UnitConversion.Application.Commands.AuthenticateUser;

namespace UnitConversion.Application.Services;

public interface IAuthService
{
    Task<AuthenticateUserResult> AuthenticateAsync(
        AuthenticateUserCommand command,
        CancellationToken cancellationToken = default);
}
