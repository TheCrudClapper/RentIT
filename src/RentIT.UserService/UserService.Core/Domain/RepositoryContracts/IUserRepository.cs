using System.Linq.Expressions;
using UserService.Core.Domain.Entities.User;
namespace UserService.Core.Domain.RepositoryContracts;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllUsersAsync(CancellationToken ct = default);
    Task<IReadOnlyCollection<User>> GetUsersByCondition(Expression<Func<User, bool>> expression, CancellationToken ct = default);
    Task<User?> GetUserByCondition(Expression<Func<User, bool>> expression, CancellationToken ct = default);
}
