using System.Net;

namespace UnitConversion.Application.Commands.AuthenticateUser;

public sealed record AuthenticateUserResult
{
    public required HttpStatusCode StatusCode { get; init; }

    public LoginResponse? Response { get; init; }

    public string? Message { get; init; }
}
