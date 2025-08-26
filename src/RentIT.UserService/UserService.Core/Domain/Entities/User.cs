using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UserService.Core.Domain.Interfaces;

namespace UserService.Core.Domain.Entities
{
    public class User : IdentityUser<Guid>, ISoftDelete
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [MaxLength(50)]
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
