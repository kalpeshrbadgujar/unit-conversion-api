using UnitConversion.Common.Enums;

namespace UnitConversion.Application.Services.Grants;

public interface IGrantAuthServiceProvider
{
    IGrantAuthService? GetService(GrantType grantType);
}
