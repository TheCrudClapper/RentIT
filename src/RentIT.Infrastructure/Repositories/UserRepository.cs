using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Infrastructure.DbContexts;

namespace RentIT.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext dbContext)
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
