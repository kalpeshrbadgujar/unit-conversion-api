using UnitConversion.Application.Commands.AuthenticateUser;

namespace UnitConversion.Application.Services.Grants;

/// <summary>
/// Strategy for a single OAuth-style grant type. Register one implementation per grant in DI.
/// </summary>
public interface IGrantAuthService
{
    GrantType GrantType { get; }

    Task<AuthenticateUserResult> AuthenticateAsync(
        AuthenticateUserCommand command,
        CancellationToken cancellationToken = default);
}
