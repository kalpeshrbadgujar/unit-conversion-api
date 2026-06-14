using System.Net;
using UnitConversion.Application.Commands.AuthenticateUser;
using UnitConversion.Domain.Auth;

namespace UnitConversion.Application.Services.Grants;

/// <summary>
/// Password grant — selected when <see cref="GrantType.Password"/> is requested.
/// </summary>
public sealed class PasswordGrantAuthService : IGrantAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public PasswordGrantAuthService(
        IUserRepository userRepository,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public GrantType GrantType => GrantType.Password;

    public async Task<AuthenticateUserResult> AuthenticateAsync(
        AuthenticateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.ValidateCredentialsAsync(
            command.Username!,
            command.Password!,
            cancellationToken);

        if (user is null)
        {
            return new AuthenticateUserResult
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Invalid username or password.",
            };
        }

        var token = _accessTokenGenerator.Generate(user.Username);

        return new AuthenticateUserResult
        {
            StatusCode = HttpStatusCode.OK,
            Response = new LoginResponse
            {
                AccessToken = token.AccessToken,
                ExpiresInMinutes = token.ExpiresInMinutes,
            },
        };
    }
}
