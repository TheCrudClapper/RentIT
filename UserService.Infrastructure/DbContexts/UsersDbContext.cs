using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Domain.Entities;

namespace UserService.Infrastructure.DbContexts
{
    public class UsersDbContext :IdentityDbContext<User, Role, Guid>
    {
        public UsersDbContext(DbContextOptions options): base(options){}
        public UsersDbContext(){}
    }
}
