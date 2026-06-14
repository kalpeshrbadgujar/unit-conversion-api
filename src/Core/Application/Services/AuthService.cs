using System.Net;
using UnitConversion.Application.Commands.AuthenticateUser;
using UnitConversion.Application.Services.Grants;

namespace UnitConversion.Application.Services;

/// <summary>
/// Resolves the grant-specific strategy at runtime and delegates authentication.
/// </summary>
public sealed class AuthService : IAuthService
{
    private readonly IGrantAuthServiceProvider _grantAuthServiceProvider;

    public AuthService(IGrantAuthServiceProvider grantAuthServiceProvider)
    {
        _grantAuthServiceProvider = grantAuthServiceProvider;
    }

    public async Task<AuthenticateUserResult> AuthenticateAsync(
        AuthenticateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var grantService = _grantAuthServiceProvider.GetService(command.GrantType);
        if (grantService is null)
        {
            return new AuthenticateUserResult
            {
                StatusCode = HttpStatusCode.NotImplemented,
                Message = $"{command.GrantType} grant is not supported yet.",
            };
        }

        return await grantService.AuthenticateAsync(command, cancellationToken);
    }
}
