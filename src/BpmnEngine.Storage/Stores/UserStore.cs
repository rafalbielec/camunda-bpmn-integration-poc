using BpmnEngine.Storage.Abstractions;
using BpmnEngine.Storage.Entities;
using Microsoft.AspNetCore.Identity;

namespace BpmnEngine.Storage.Stores;

public class UserStore : IUserPasswordStore<UserEntity>
{
    private readonly IUsersRepository _repository;

    public UserStore(IUsersRepository repository)
    {
        _repository = repository;
    }

    public void Dispose() { }

    public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(new IdentityResult());
    }

    public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(new IdentityResult());
    }

    public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(new IdentityResult());
    }

    public async Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return (await _repository.SelectByIdString(userId))!;
    }

    public async Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return (await _repository.SelectByUserName(normalizedUserName))!;
    }

    public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}