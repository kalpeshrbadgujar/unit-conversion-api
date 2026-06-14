using UnitConversion.Application.Commands.AuthenticateUser;

namespace UnitConversion.Application.Services.Grants;

public interface IGrantAuthServiceProvider
{
    IGrantAuthService? GetService(GrantType grantType);
}
