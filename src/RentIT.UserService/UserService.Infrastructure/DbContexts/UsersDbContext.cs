using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Domain.Entities;
using UserService.Core.Domain.Interfaces;
using UserService.Infrastructure.DbContexts.Interceptors;

namespace UserService.Infrastructure.DbContexts
{
    public class UsersDbContext :IdentityDbContext<User, Role, Guid>
    {
        public UsersDbContext(DbContextOptions options): base(options){}
        public UsersDbContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasQueryFilter(item => item.IsActive);
        }
    }
}
