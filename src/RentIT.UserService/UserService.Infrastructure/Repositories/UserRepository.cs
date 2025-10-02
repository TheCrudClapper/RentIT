using Microsoft.EntityFrameworkCore;
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

    public Task<bool> DoesUserExistsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _context.Users
            .AnyAsync(item => item.Id == userId,cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllActiveUsersAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
