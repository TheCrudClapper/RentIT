using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserService.Core.Domain.Entities;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Infrastructure.DbContexts;

namespace UserService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;
    public UserRepository(UsersDbContext dbContext)
    {
        _context = dbContext;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByCondition(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsersByCondition(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(expression)
            .ToListAsync(cancellationToken);
    }
}
