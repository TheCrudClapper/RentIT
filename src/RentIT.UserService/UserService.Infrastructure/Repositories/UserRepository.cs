using Microsoft.EntityFrameworkCore;
using UserService.Core.Domain.Entities;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Infrastructure.DbContexts;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;
        public UserRepository(UsersDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task<bool> DoesUserExistsAsync(Guid userId)
        {
            return _context.Users
                .AnyAsync(item => item.IsActive && item.Id == userId);
        }

        public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
        {
            return await _context.Users.Where(item => item.IsActive)
                .ToListAsync();
        }
    }
}
