using UnitConversion.Common.Enums;

namespace UnitConversion.Application.Commands.AuthenticateUser;

public sealed record AuthenticateUserCommand(
    GrantType GrantType,
    string? Username,
    string? Password);
