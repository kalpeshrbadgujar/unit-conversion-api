using UnitConversion.Domain.Models;

namespace UnitConversion.Domain.Auth;

/// <summary>
/// User lookup and credential validation. In-memory today; Can be swapped for real time DB implementation later.
/// </summary>
public interface IUserRepository
{
    Task<UserAccount?> ValidateCredentialsAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default);
}
