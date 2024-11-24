using BpmnEngine.Storage.Entities;

namespace BpmnEngine.Storage.Abstractions;

public interface IUsersRepository
{
    Task<UserEntity?> SelectById(Guid id);
    Task<UserEntity?> SelectByIdString(string id);
    Task<UserEntity?> SelectByUserName(string userName);
}