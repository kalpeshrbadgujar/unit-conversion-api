using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UnitConversion.Domain.Auth;
using UnitConversion.Domain.Models;

namespace UnitConversion.Infrastructure.Authentication;

public sealed class JwtAccessTokenGenerator : IAccessTokenGenerator
{
    private readonly JwtOptions _options;

    public JwtAccessTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public AccessTokenResult Generate(string username)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
            signingCredentials: credentials);

        return new AccessTokenResult
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresInMinutes = _options.ExpirationMinutes,
        };
    }
}
