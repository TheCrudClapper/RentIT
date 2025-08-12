using UserService.Core.Domain.Entities;
namespace UserService.Core.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllActiveUsersAsync();

        Task<bool> DoesUserExistsAsync(Guid userId);
    }
}
