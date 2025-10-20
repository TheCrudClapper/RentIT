using System.Linq.Expressions;
using UserService.Core.Domain.Entities;
namespace UserService.Core.Domain.RepositoryContracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression);
    Task<User?> GetUserByCondition(Expression<Func<User, bool>> expression);
    Task<bool> DoesUserExistsAsync(Guid userId, CancellationToken cancellationToken);

    Task<User>
}
