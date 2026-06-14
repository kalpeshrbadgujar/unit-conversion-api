using UnitConversion.Domain.Models;

namespace UnitConversion.Domain.Auth;

public interface IAccessTokenGenerator
{
    AccessTokenResult Generate(string username);
}
