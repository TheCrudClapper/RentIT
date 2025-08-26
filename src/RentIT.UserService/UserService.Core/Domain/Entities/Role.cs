using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Interfaces;
namespace UserService.Core.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
    }
}
