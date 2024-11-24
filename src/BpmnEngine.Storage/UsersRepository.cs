using BpmnEngine.Storage.Abstractions;
using BpmnEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace BpmnEngine.Storage;

public class UsersRepository : IUsersRepository
{
    private readonly string _connectionString;

    public UsersRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<UserEntity?> SelectById(Guid id)
    {
        await using var context = new StorageContext(_connectionString);

        return await context.Users.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);
    }
    
    public Task<UserEntity?> SelectByIdString(string id)
    {
        return Guid.TryParse(id, out var g) ? SelectById(g) : null!;
    }

    public async Task<UserEntity?> SelectByUserName(string userName)
    {
        await using var context = new StorageContext(_connectionString);

        var u = userName.ToLowerInvariant();
        return await context.Users.AsNoTracking().SingleOrDefaultAsync(a => a.UserName == u);
    }
}