using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserService.Core.Domain.Entities.User;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Infrastructure.DbContexts;

namespace UserService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;
    public UserRepository(UsersDbContext dbContext)
        => _context = dbContext;
    
    public async Task<IReadOnlyCollection<User>> GetAllUsersAsync(CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<User?> GetUserByCondition(Expression<Func<User, bool>> expression, CancellationToken ct)
    {
        return await _context.Users
            .FirstOrDefaultAsync(expression, ct);
    }

    public async Task<IReadOnlyCollection<User>> GetUsersByCondition(Expression<Func<User, bool>> expression, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(expression)
            .ToListAsync(ct);
    }
}
