using UnitConversion.Domain.Auth;
using UnitConversion.Domain.Models;

namespace UnitConversion.Infrastructure.Persistence;

internal sealed class InMemoryUserRepository : IUserRepository
{
    private readonly InMemoryDataContext _dataContext;

    public InMemoryUserRepository(InMemoryDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<UserAccount?> ValidateCredentialsAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = _dataContext.Users.FirstOrDefault(record =>
            string.Equals(record.Username, username, StringComparison.Ordinal)
            && string.Equals(record.Password, password, StringComparison.Ordinal));

        if (user is null)
        {
            return Task.FromResult<UserAccount?>(null);
        }

        return Task.FromResult<UserAccount?>(new UserAccount
        {
            Id = user.Id,
            Username = user.Username,
        });
    }
}
