using System.Linq.Expressions;
using UserService.Core.Domain.Entities;
namespace UserService.Core.Domain.RepositoryContracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default);
    Task<User?> GetUserByCondition(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default);
}
